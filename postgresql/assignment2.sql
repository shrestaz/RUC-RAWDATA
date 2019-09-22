-- GROUP: raw4, MEMBERS: Ozge Yasayan, Shushma Devi Gurung, Ivan Spajic, Manish Shrestha,

\timing


-- a)
DROP FUNCTION IF EXISTS title_count;
CREATE FUNCTION title_count(actor_name TEXT)
    RETURNS INT AS
$$
DECLARE
    kevin_bacon_count INTEGER;
BEGIN
    SELECT COUNT
               (DISTINCT movie_id)
    INTO kevin_bacon_count
    FROM casting AS c,
         person AS p,
         movie AS m
    WHERE c.person_id = p.ID
      AND c.movie_id = m.ID
      AND m.kind = 'movie'
      AND c.role_type = 'actor'
      AND p.NAME LIKE actor_name;
    RETURN kevin_bacon_count;

END;
$$ LANGUAGE plpgsql;

SELECT title_count('Bacon, Kevin');


-- b)
DROP FUNCTION IF EXISTS title_count;
CREATE FUNCTION title_count(actor_name TEXT, movie_kind TEXT, casting_role_type TEXT)
    RETURNS INT AS
$$
DECLARE
    kevin_bacon_count INTEGER;
BEGIN
    SELECT COUNT(DISTINCT movie_id)
    INTO kevin_bacon_count
    FROM casting AS c,
         person AS p,
         movie AS m
    WHERE c.person_id = p.ID
      AND c.movie_id = m.ID
      AND m.kind = movie_kind
      AND c.role_type = casting_role_type
      AND p.NAME LIKE actor_name;
    RETURN kevin_bacon_count;

END;
$$ LANGUAGE plpgsql;

SELECT title_count('Bacon, Kevin', 'episode', 'actor');


-- c)
DROP FUNCTION IF EXISTS title_count;
CREATE FUNCTION title_count(actor_name TEXT, movie_kind TEXT = 'movie', casting_role_type TEXT = 'actor')
    RETURNS INT AS
$$
DECLARE
    kevin_bacon_count INTEGER;
BEGIN
    SELECT COUNT(DISTINCT movie_id)
    INTO kevin_bacon_count
    FROM casting AS c,
         person AS p,
         movie AS m
    WHERE c.person_id = p.ID
      AND c.movie_id = m.ID
      AND m.kind = movie_kind
      AND c.role_type = casting_role_type
      AND p.NAME LIKE actor_name;
    RETURN kevin_bacon_count;

END;
$$ LANGUAGE plpgsql;

SELECT title_count('Bacon, Kevin');


-- d)
DROP FUNCTION IF EXISTS movies;
CREATE FUNCTION movies(actor_name TEXT)
    RETURNS TABLE
            (
                movie_production_year INT,
                movie_title           TEXT
            )
AS
$$
BEGIN
    RETURN query SELECT m.production_year, m.title
                 FROM movie AS m
                          JOIN casting AS c ON m.ID = c.movie_id
                          JOIN person AS p ON c.person_id = p.ID
                 WHERE p.name LIKE actor_name
                   AND m.kind = 'movie';

END;
$$ LANGUAGE plpgsql;

SELECT movies('Fairlie, Ann');


-- e)
DROP FUNCTION IF EXISTS match_title;
CREATE FUNCTION match_title(w TEXT)
    RETURNS TABLE
            (
                movie_production_year INT,
                movie_title           TEXT
            )
AS
$$
BEGIN
    RETURN query
        SELECT production_year,
               title
        FROM movie
        WHERE kind = 'movie'
          AND title LIKE '%' || w || '%'
          AND production_year IS NOT NULL
        ORDER BY production_year DESC
        LIMIT 10;
END;
$$ LANGUAGE plpgsql;

SELECT *
FROM match_title('Wonderful');


-- f)
DROP FUNCTION IF EXISTS collect_role_types;
CREATE FUNCTION collect_role_types(person_name TEXT)
    RETURNS TEXT AS
$$
DECLARE
    role_types_result     TEXT DEFAULT '';
    role_types_collection RECORD;
    query_cursor CURSOR ( person_name TEXT ) FOR
        SELECT DISTINCT role_type
        FROM casting,
             person
        WHERE casting.person_id = person.ID
          AND NAME LIKE person_name;
BEGIN
    -- Open the cursor
    OPEN query_cursor ( person_name );
    LOOP
        -- Fetch row into the RECORD role_types_collection
        FETCH query_cursor INTO role_types_collection;
        -- Exit when no more row to fetch
        EXIT
            WHEN NOT FOUND;

        -- Build the output
        role_types_result := role_types_result || ',' || role_types_collection.role_type;
    END LOOP;
    -- Trim the leading ','
    role_types_result = trim(LEADING ',' FROM role_types_result);
    -- Close the cursor
    CLOSE query_cursor;
    RETURN role_types_result;
END;
$$ LANGUAGE plpgsql;

SELECT collect_role_types('Bacon, Kevin');

SELECT NAME, collect_role_types(NAME)
FROM person
WHERE NAME LIKE 'De Niro, R%';


-- g)
DROP FUNCTION IF EXISTS collect_role_types;
CREATE FUNCTION collect_role_types(person_name TEXT)
    RETURNS TEXT AS
$$
DECLARE
    role_types_result     text DEFAULT '';
    role_types_collection RECORD;
BEGIN
    FOR role_types_collection IN
        SELECT DISTINCT role_type
        FROM casting,
             person
        WHERE casting.person_id = person.ID
          AND NAME LIKE person_name
        LOOP
            role_types_result := role_types_result || ', ' || role_types_collection.role_type;
        END LOOP;
    role_types_result = trim(LEADING ', ' FROM role_types_result);
    RETURN role_types_result;
END;
$$ LANGUAGE plpgsql;

SELECT collect_role_types('Bacon, Kevin');

SELECT NAME, collect_role_types(NAME)
FROM person
WHERE NAME LIKE 'De Niro, R%';


-- h)

-- Reset table casting by removing the new roles added by previous rubn for the purpose of running the script multiple times
DELETE
FROM casting
WHERE ID IN (49502700, 49502701);

-- Drop the function from g) if exists, refactor it to take actor_id as argument
DROP FUNCTION IF EXISTS collect_role_types;

-- Create function which returns all the roles of a given person by its id
CREATE FUNCTION collect_role_types(actor_id int)
    RETURNS TEXT AS
$$
DECLARE
    role_types_result     text DEFAULT '';
    role_types_collection RECORD;
BEGIN
    FOR role_types_collection IN
        SELECT DISTINCT role_type
        FROM casting,
             person
        WHERE casting.person_id = person.ID
          AND person.id = actor_id
        LOOP
            role_types_result := role_types_result || ', ' || role_types_collection.role_type;
        END LOOP;
    role_types_result = trim(LEADING ', ' FROM role_types_result);
    RETURN role_types_result;
END;
$$ LANGUAGE plpgsql;

-- Remove role_types from person if exists; for running the script multiple times
ALTER TABLE person
    drop column if exists role_types;

-- Add column role_types on person as required by the question
ALTER TABLE person
    ADD role_types TEXT;

-- Update person table with their role_types only for persons with ids 1372139 and 103491
UPDATE person
SET role_types = role_types_collection.collect_role_types
FROM (SELECT ID, NAME, collect_role_types(ID)
      FROM person
      WHERE ID IN (103491, 1372139)) role_types_collection
WHERE person.ID = role_types_collection.ID;

-- Drop trigger if exists from previous run
DROP TRIGGER IF EXISTS update_role_types ON person;

-- Drop function if exists from previous run
DROP FUNCTION IF EXISTS update_redundant_data CASCADE;

-- Create trigger
CREATE FUNCTION update_redundant_data()
    RETURNS TRIGGER AS
$$
BEGIN
    UPDATE person
    SET role_types = role_types || ', ' || NEW.role_type
    WHERE person.ID = NEW.person_id;
    RETURN NULL;
END
$$ LANGUAGE plpgsql;

-- Define when to execute trigger
CREATE TRIGGER update_role_types
    AFTER INSERT
    ON casting
    FOR EACH ROW
EXECUTE PROCEDURE update_redundant_data();

-- Execute test queries from the question
SELECT NAME, role_types
FROM person
WHERE ID IN (1372139, 103491);

INSERT INTO casting (ID, person_id, movie_id, role_type)
VALUES (49502700, 1372139, 2440185, 'director');

INSERT INTO casting (ID, person_id, movie_id, role_type)
VALUES (49502701, 103491, 2400814, 'production designer');

SELECT NAME, role_types
FROM person
WHERE ID IN (1372139, 103491);