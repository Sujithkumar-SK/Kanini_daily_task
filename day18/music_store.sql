create database MusicStores;
use MusicStores;

create table Genre (
	Genreid int primary key,
	Name varchar(20)
);

create table Artist (
	ArtistId int primary key,
	Name varchar(20)
	);
create table Album (
	AlbumId int primary key,
	Title varchar(100),
	ArtistId int,
	foreign key (ArtistId) references Artist(ArtistId)
	);

create table Track (
	TrackId int primary key,
	Name varchar(50),
	AlbumId int,
	GenreId int,
	Composer varchar(100),
	foreign key (GenreId) references Genre(Genreid),
	foreign key (AlbumId) references Album(AlbumId)
	);

INSERT INTO Genre VALUES
(1, 'Rock'),
(2, 'Jazz'),
(3, 'Metal'),
(4, 'Alternative & Punk'),
(5, 'Rock And Roll'),
(6, 'Blues'),
(7, 'Lati'),
(8, 'Reggae'),
(9, 'Pop'),
(10, 'Soundtrack');

INSERT INTO Artist VALUES
(1, 'AC/DC'),
(2, 'Accept'),
(3, 'Aerosmith'),
(4, 'Alanis Morissette'),
(5, 'Alice In Chains'),
(6, 'Antônio Carlos Jobim'),
(7, 'Apocalyptica'),
(8, 'Audioslave'),
(9, 'BackBeat'),
(10, 'Billy Cobham');

INSERT INTO Album VALUES
(1, 'For Those About To Rock We Salute You', 1),
(2, 'Balls to the Wall', 2),
(3, 'Restless and Wild', 2),
(4, 'Let There Be Rock', 1),
(5, 'Big Ones', 3),
(6, 'Jagged Little Pill', 4),
(7, 'Facelift', 5),
(8, 'Warner 25 Anos', 6),
(9, 'Plays Metallica By Four Cellos', 7),
(10, 'Audioslave', 8),
(11, 'Out Of Exile', 8),
(12, 'BackBeat Soundtrack', 9),
(13, 'The Best Of Billy Cobham', 10);

INSERT INTO Track VALUES
(1, 'For Those About To Rock (We Salute You)', 1, 1, 'Angus Young, Malcolm Young, Brian Johnson'),
(2, 'Balls to the Wall', 2, 1, NULL),
(3, 'Fast As a Shark', 3, 1, 'F. Baltes, S. Kaufman, U. Dirkscneider & W. Hoffman'),
(4, 'Restless and Wild', 3, 1, 'F. Baltes, R.A. Smith-Diesel, S. Kaufman, U. Dirkscneider'),
(5, 'Princess of the Dawn', 3, 1, 'Deaffy & R.A. Smith-Diesel'),
(6, 'Put The Finger On You', 1, 1, 'Angus Young, Malcolm Young, Brian Johnson'),
(7, 'Let’s Get It Up', 1, 2, 'Angus Young, Malcolm Young, Brian Johnson'),
(8, 'Inject The Venom', 1, 1, 'Angus Young, Malcolm Young, Brian Johnson'),
(9, 'Snowballed', 1, 1, 'Angus Young, Malcolm Young, Brian Johnson'),
(10, 'Evil Walks', 1, 1, 'Angus Young, Malcolm Young, Brian Johnson'),
(11, 'C.O.D.', 1, 1, 'Angus Young, Malcolm Young, Brian Johnson'),
(12, 'Breaking The Rules', 1, 3, 'Angus Young, Malcolm Young, Brian Johnson'),
(13, 'Night Of The Long Knives', 1, 2, 'Angus Young, Malcolm Young, Brian Johnson'),
(14, 'Spellbound', 1, 3, 'Angus Young, Malcolm Young, Brian Johnson'),
(15, 'Go Down', 4, 5, 'AC/DC'),
(16, 'Dog Eat Dog', 4, 6, 'AC/DC'),
(17, 'Let There Be Rock', 4, 2, 'AC/DC'),
(18, 'Bad Boy Boogie', 4, 3, 'AC/DC'),
(19, 'Problem Child', 4, 7, 'AC/DC'),
(20, 'Overdose', 4, 1, 'AC/DC'),
(21, 'Hell Ain’t A Bad Place To Be', 4, 1, 'AC/DC');

--1. NoofTracks
select a.Title as AlbumTitle, count(t.TrackId) as NoofTracks
from Album a
join Track t on a.AlbumId = t.AlbumId
group by a.Title
order by NoofTracks asc

--2.ends with m
select 
	ar.Name as ArtistName,
	al.Title as AlbumTitle
from
Artist ar join Album al on ar.ArtistId = al.ArtistId
where ar.Name like '%m'
order by ar.name;