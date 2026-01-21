CREATE DATABASE IF NOT EXISTS biuro_podrozy;
USE biuro_podrozy;

CREATE TABLE biura (
    id_biura INT PRIMARY KEY AUTO_INCREMENT,
    nazwa VARCHAR(100) NOT NULL,
    miasto VARCHAR(50),
    adres VARCHAR(150)
);

CREATE TABLE pracownicy (
    id_pracownika INT PRIMARY KEY AUTO_INCREMENT,
    imie VARCHAR(50) NOT NULL,
    nazwisko VARCHAR(50) NOT NULL,
    id_biura INT,
    FOREIGN KEY (id_biura) REFERENCES biura(id_biura)
);

CREATE TABLE klienci (
    id_klienta INT PRIMARY KEY AUTO_INCREMENT,
    imie VARCHAR(50),
    nazwisko VARCHAR(50),
    email VARCHAR(100) UNIQUE,
    telefon VARCHAR(15)
);

CREATE TABLE hotele (
    id_hotelu INT PRIMARY KEY AUTO_INCREMENT,
    nazwa_hotelu VARCHAR(100) NOT NULL,
    lokalizacja VARCHAR(100),
    standard INT
);

CREATE TABLE wycieczki (
    id_wycieczki INT PRIMARY KEY AUTO_INCREMENT,
    cel VARCHAR(100),
    cena DECIMAL(10, 2),
    id_hotelu INT,
    FOREIGN KEY (id_hotelu) REFERENCES hotele(id_hotelu)
);

CREATE TABLE rezerwacje (
    id_rezerwacji INT PRIMARY KEY AUTO_INCREMENT,
    id_klienta INT,
    id_wycieczki INT,
    id_pracownika INT,
    data_rezerwacji DATE,
    FOREIGN KEY (id_klienta) REFERENCES klienci(id_klienta),
    FOREIGN KEY (id_wycieczki) REFERENCES wycieczki(id_wycieczki),
    FOREIGN KEY (id_pracownika) REFERENCES pracownicy(id_pracownika)
);

CREATE TABLE platnosci (
    id_platnosci INT PRIMARY KEY AUTO_INCREMENT,
    kwota DECIMAL(10, 2),
    data_platnosci DATE,
    id_rezerwacji INT,
    FOREIGN KEY (id_rezerwacji) REFERENCES rezerwacje(id_rezerwacji)
);