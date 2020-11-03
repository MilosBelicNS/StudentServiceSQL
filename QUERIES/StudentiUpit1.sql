use DotNetMilos

--select ime, prezime, naziv_leta, kompanija 
--	from putnici inner join vezna  on putnici.id = vezna.id_putnika
--	             inner join letovi on letovi.id  = vezna.id_leta;


--Preuzima studente zajedno sa predmetima koje pohađaju. Na spisku treba da budu
--samo studenti koji su uključeni u nastavu (pohađaju neki predmet)
select * from studenti
inner join pohadja on studenti.student_id = pohadja.student_id
inner join predmeti on predmeti.predmet_id = pohadja.predmet_id;                     