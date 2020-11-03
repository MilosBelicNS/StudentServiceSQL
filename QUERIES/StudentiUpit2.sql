use DotNetMilos

--Preuzima SVE studente zajedno sa predmetima koje pohađaju. Upit treba da selektuje
--studenta iako ne pohadja nijedan predmet


select *
	from studenti left join pohadja  on studenti.student_id = pohadja.student_id
	             left join predmeti on pohadja.predmet_id  = pohadja.predmet_id;