-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema realestate
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema realestate
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `realestate` DEFAULT CHARACTER SET utf8 ;
USE `realestate` ;

-- -----------------------------------------------------
-- Table `realestate`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`User` (
  `idUser` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(50) NOT NULL,
  `LastName` VARCHAR(50) NOT NULL,
  `Username` VARCHAR(30) NOT NULL,
  `Password` VARCHAR(513) NOT NULL,
  `Salt` VARCHAR(50) NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE INDEX `Username_UNIQUE` (`Username` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Administrator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Administrator` (
  `Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`Korisnik_idKorisnik`),
  CONSTRAINT `fk_Administrator_Korisnik1`
    FOREIGN KEY (`Korisnik_idKorisnik`)
    REFERENCES `realestate`.`User` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Client`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Client` (
  `Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`Korisnik_idKorisnik`),
  CONSTRAINT `fk_Klijent_Korisnik`
    FOREIGN KEY (`Korisnik_idKorisnik`)
    REFERENCES `realestate`.`User` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Agent`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Agent` (
  `Korisnik_idKorisnik` INT NOT NULL,
  `PhoneNumber` VARCHAR(15) NOT NULL,
  `Activated` TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (`Korisnik_idKorisnik`),
  CONSTRAINT `fk_Agent_Korisnik1`
    FOREIGN KEY (`Korisnik_idKorisnik`)
    REFERENCES `realestate`.`User` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Address` (
  `idAddress` INT NOT NULL AUTO_INCREMENT,
  `Number` VARCHAR(10) NOT NULL,
  `Street` VARCHAR(200) NOT NULL,
  `City` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idAddress`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`RealestateType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`RealestateType` (
  `idRealesateType` INT NOT NULL AUTO_INCREMENT,
  `RealestateType` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`idRealesateType`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Realestate`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Realestate` (
  `idRealestate` INT NOT NULL AUTO_INCREMENT,
  `Description` MEDIUMTEXT NOT NULL,
  `Adresa_idAdresa` INT NOT NULL,
  `TipNekretnine_idTipNekretnine` INT NOT NULL,
  `SquareFootage` VARCHAR(45) NOT NULL,
  `ImagePaths` JSON NOT NULL,
  `DateAdded` DATE NOT NULL,
  PRIMARY KEY (`idRealestate`),
  INDEX `fk_Nekretnina_Adresa1_idx` (`Adresa_idAdresa` ASC) VISIBLE,
  INDEX `fk_Nekretnina_TipNekretnine1_idx` (`TipNekretnine_idTipNekretnine` ASC) VISIBLE,
  CONSTRAINT `fk_Nekretnina_Adresa1`
    FOREIGN KEY (`Adresa_idAdresa`)
    REFERENCES `realestate`.`Address` (`idAddress`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Nekretnina_TipNekretnine1`
    FOREIGN KEY (`TipNekretnine_idTipNekretnine`)
    REFERENCES `realestate`.`RealestateType` (`idRealesateType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`OfferType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`OfferType` (
  `idType` INT NOT NULL AUTO_INCREMENT,
  `OfferType` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`idType`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Offer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Offer` (
  `idOffer` INT NOT NULL AUTO_INCREMENT,
  `Tip_idTip` INT NOT NULL,
  `Agent_Korisnik_idKorisnik` INT NOT NULL,
  `Price` DECIMAL NOT NULL,
  `ShortDescription` TEXT(1000) NOT NULL,
  `Title` TINYTEXT NOT NULL,
  `Active` TINYINT NOT NULL DEFAULT 1,
  `Realestate_idRealestate` INT NOT NULL,
  `DateAdded` DATE NOT NULL,
  INDEX `fk_Oglas_Tip1_idx` (`Tip_idTip` ASC) VISIBLE,
  PRIMARY KEY (`idOffer`),
  INDEX `fk_Oglas_Agent1_idx` (`Agent_Korisnik_idKorisnik` ASC) VISIBLE,
  INDEX `fk_Offer_Realestate1_idx` (`Realestate_idRealestate` ASC) VISIBLE,
  CONSTRAINT `fk_Oglas_Tip1`
    FOREIGN KEY (`Tip_idTip`)
    REFERENCES `realestate`.`OfferType` (`idType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Oglas_Agent1`
    FOREIGN KEY (`Agent_Korisnik_idKorisnik`)
    REFERENCES `realestate`.`Agent` (`Korisnik_idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Offer_Realestate1`
    FOREIGN KEY (`Realestate_idRealestate`)
    REFERENCES `realestate`.`Realestate` (`idRealestate`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Contract`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Contract` (
  `idContract` INT NOT NULL AUTO_INCREMENT,
  `Content` MEDIUMTEXT NOT NULL,
  `SignDate` DATE NOT NULL,
  `PeriodVazenja` INT NOT NULL,
  `Offer_idOffer` INT NOT NULL,
  `Client_Korisnik_idKorisnik` INT NOT NULL,
  PRIMARY KEY (`idContract`),
  INDEX `fk_Contract_Offer1_idx` (`Offer_idOffer` ASC) VISIBLE,
  INDEX `fk_Contract_Client1_idx` (`Client_Korisnik_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_Contract_Offer1`
    FOREIGN KEY (`Offer_idOffer`)
    REFERENCES `realestate`.`Offer` (`idOffer`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Contract_Client1`
    FOREIGN KEY (`Client_Korisnik_idKorisnik`)
    REFERENCES `realestate`.`Client` (`Korisnik_idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `realestate`.`Inquiry`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `realestate`.`Inquiry` (
  `idInquiry` INT NOT NULL AUTO_INCREMENT,
  `Message` MEDIUMTEXT NOT NULL,
  `Client_Korisnik_idKorisnik` INT NOT NULL,
  `Offer_idOffer` INT NOT NULL,
  PRIMARY KEY (`idInquiry`),
  INDEX `fk_Inquiry_Client1_idx` (`Client_Korisnik_idKorisnik` ASC) VISIBLE,
  INDEX `fk_Inquiry_Offer1_idx` (`Offer_idOffer` ASC) VISIBLE,
  CONSTRAINT `fk_Inquiry_Client1`
    FOREIGN KEY (`Client_Korisnik_idKorisnik`)
    REFERENCES `realestate`.`Client` (`Korisnik_idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inquiry_Offer1`
    FOREIGN KEY (`Offer_idOffer`)
    REFERENCES `realestate`.`Offer` (`idOffer`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
