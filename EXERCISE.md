Original version is here http://matteo.vaccari.name/blog/archives/154.html

### Problem
We are a very thoughtful company (ACME), and we want to develop a CLI application that send a greeting email to all our  employees whose birthday is today.

The employees are stored in a CSV file (employees.csv) with the following format:

```text
last_name, first_name, date_of_birth, email
Capone, Al, 1951-10-08, al.capone@acme.com
Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com
Wick, John, 1987-09-11, john.wick@acme.com
```

The greetings email should contain the following text:

```text
Subject: Happy birthday!

Happy birthday, dear John!
```

with the first name of the employee substituted for `John`

Obviously we want to develop in a TDD style but this time let's go for a different approach using a more **outside-in** point of view.

Have fun!

---

https://github.com/cmendible/netDumbster
https://mailpit.axllent.org/docs/install/#docker

