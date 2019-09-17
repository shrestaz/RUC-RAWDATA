-- GROUP: raw4, MEMBERS: Ozge Yasayan, Shushma Devi Gurung, Ivan Spajic, Manish Shrestha,

\timing


-- a)
select title, production_year
from movie
where title like 'Pirates of the Caribbean%'
  and kind = 'movie';


-- b)
select count(*)
from movie
where production_year = 2004
  and kind = 'movie';


-- c)
select title
from casting
         join
     movie on casting.movie_id = movie.id
         join person on casting.person_id = person.id
where person.name = 'Mikkelsen, Mads'
  and movie.kind = 'video game';


-- d)
select distinct role_type
from casting
         join person
              on casting.person_id = person.id
where person.name = 'Bacon, Kevin';


-- e)
select role_type, count(*)
from casting
group by role_type
order by count DESC;


-- f)
select title
from movie
         join casting c on movie.id = c.movie_id
         join person p on c.person_id = p.id
where name = 'Scott, Ridley'
  and role_type = 'director'
  and production_year in (2004, 2006, 2008, 2010);


-- g.
select max(actor_count)
from (select count(person_id) as actor_count
      from casting
      where role_type = 'actor'
      group by movie_id) movie_casting;


-- h)
select count(distinct person_id)
from (select movie_id as mid, person_id as pid
      from casting
               join person p on casting.person_id = p.id
      where name = 'Bacon, Kevin'
        and casting.role_type = 'actor'
      group by movie_id, person_id) kb_movies
         join casting c on c.movie_id = mid
where role_type = 'actor'
  and person_id != pid;


-- i)
select title
from movie_keyword
         join keyword k on movie_keyword.keyword_id = k.id
         join movie m on movie_keyword.movie_id = m.id
where k.keyword = 'elephant-fears-mouse';


-- j)
select title
from movie_keyword
         join keyword k on movie_keyword.keyword_id = k.id
         join movie m on movie_keyword.movie_id = m.id
where k.keyword = 'dancing'
intersect
select title
from movie_keyword
         join keyword k on movie_keyword.keyword_id = k.id
         join movie m on movie_keyword.movie_id = m.id
where k.keyword = 'elephant-fears-mouse';


-- k)
select title, production_year
from movie_company
         join company c on movie_company.company_id = c.id
         join movie m on movie_company.movie_id = m.id
where c.name = 'Paramount'
  and c.country_code = '[se]'
  and production_year > 2004;


-- l)
select title
from movie
         join casting c on movie.id = c.movie_id
         join role r on c.role_id = r.id
where r.name = 'The Singing Kid';


-- m)
select p.name, m.title
from person p
         join casting c on p.id = c.person_id
         join role r on c.role_id = r.id
         join movie m on c.movie_id = m.id
where r.name = 'Bilbo'
  and m.title like '%Smaug%';
