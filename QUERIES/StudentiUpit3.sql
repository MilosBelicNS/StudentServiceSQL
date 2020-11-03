use DotNetMilos

--Preuzima ime, prezime i broj predmeta koje pohađa za svakog studentaselect ime, prezime, predmeti.predmet_id from studentiinner join pohadja on studenti.student_id = pohadja.student_idinner join predmeti on predmeti.predmet_id = pohadja.predmet_id;--select ime, prezime, naziv_leta, kompanija 
--	from putnici inner join vezna  on putnici.id = vezna.id_putnika
--	             inner join letovi on letovi.id  = vezna.id_leta;