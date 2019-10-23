# <img src="https://ruc.dk/sites/default/files/2017-05/ruc_logo_download_dk.png" width=500px>


## RAWDATA Assignment 3 – Network

This assignment concerns development of a network service using the RDJTP – RawData JSON Transport Protocol.
The task is to create a network service that provide the functionality defined by the RDJTP using Test Driven Development (TDD).

[Assignment description](https://github.com/ozgey99/Assignment3/blob/master/Assignment%20Description.pdf)

[Assignment hand-in document](https://github.com/ozgey99/Assignment3/blob/master/RAW4%20Assignment%203.pdf)

This network service was developed by group **raw4** of course RAWDATA (Master's in Computer Science, Roskilde University):
- [Özge Yaşayan](https://github.com/ozgey99)
- [Shushma Devi Gurung](https://github.com/shus0001)
- [Ivan Spajić](https://github.com/ivanspajic)
- [Manish Shrestha](https://github.com/shrestaz)

## Current status: All tests pass (28 out of 28)

### Screenshots of test output:

1. Terminal:

<img src="Screenshots/Terminal-testing.png" width=500px>

2. Test Explorer:
<img src="Screenshots/Test-Explorer.png" width=300px>

3. Visual Studio:
<img src="Screenshots/Full-view.png" width=500px>



## Steps to reproduce:

> **Prerequisites: You must have [Git](https://git-scm.com/downloads) and [.Net Core 3.0 SDK](https://dotnet.microsoft.com/download) installed. Use OS and IDE of your choice.**

0. `git clone https://github.com/ozgey99/Assignment3.git`

### Visual Studio:

1. Open Project or Solution > Navigate to the cloned folder and open the solution named `Assignment3TestSuite.sln`.

2. Start the server by pressing `CTRL + F5` or **Debug** menu> **Start Without Debugging**.

3. Run the test by opening **Test** menu > Run All Tests.

### Terminal:

1. Navigate into the folder containing server code `cd Assignment3/Assignment3`

2. Start the server `dotnet run`

3. In a new terminal, navigate to the cloned folder `cd Assignment3`

4. Start the test `dotnet test`.


Happy Coding! 👨‍💻 👩‍💻


