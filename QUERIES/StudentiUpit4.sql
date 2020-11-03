
use DotNetMilos


--Za svakog studenta koliko je najviše bodova imao na teoriji

--max teorija upit
select  ime, prezime, MAX(ispitne_prijave.teorija) as maksTeorija from studenti
inner join ispitne_prijave on studenti.student_id = ispitne_prijave.student_id group by ime, prezime;

--upit koji izbacuje sve studente koji su prijavili ispit sa brojem bodova iz teorije
select  ime, prezime, ispitne_prijave.teorija from studenti
inner join ispitne_prijave on studenti.student_id = ispitne_prijave.student_id;







