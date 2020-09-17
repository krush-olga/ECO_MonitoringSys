-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: experts
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `calculations_description`
--

DROP TABLE IF EXISTS `calculations_description`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `calculations_description` (
  `calculation_number` int(11) NOT NULL,
  `calculation_name` varchar(250) DEFAULT NULL,
  `description_of_calculation` varchar(500) DEFAULT NULL,
  `issue_id` int(11) DEFAULT NULL,
  `id_of_expert` int(11) NOT NULL,
  PRIMARY KEY (`calculation_number`,`id_of_expert`),
  UNIQUE KEY `calculation_name_UNIQUE` (`calculation_name`),
  KEY `fk_issue_id_idx` (`issue_id`),
  KEY `fk_id_of_exp_calc_desc_idx` (`id_of_expert`),
  CONSTRAINT `fk_id_of_exp_calc_desc` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_issue_id` FOREIGN KEY (`issue_id`) REFERENCES `issues` (`issue_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `calculations_description`
--

LOCK TABLES `calculations_description` WRITE;
/*!40000 ALTER TABLE `calculations_description` DISABLE KEYS */;
INSERT INTO `calculations_description` VALUES (2,'Княжичі (економіст)','тестетс',12,1),(2,'a31','b31',12,3),(3,'123','',13,1);
/*!40000 ALTER TABLE `calculations_description` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `calculations_result`
--

DROP TABLE IF EXISTS `calculations_result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `calculations_result` (
  `calculation_number` int(11) NOT NULL,
  `date_of_calculation` datetime DEFAULT NULL,
  `id_of_formula` int(11) NOT NULL,
  `result` double DEFAULT NULL,
  `id_of_expert` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`calculation_number`,`id_of_formula`,`id_of_expert`),
  KEY `fk_formula2_id_idx` (`id_of_formula`),
  KEY `fk_id_of_exp_calc_res_idx` (`id_of_expert`),
  CONSTRAINT `fk_calc_numb_desc` FOREIGN KEY (`calculation_number`) REFERENCES `calculations_description` (`calculation_number`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_id_of_exp_calc_res` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_id_of_formula` FOREIGN KEY (`id_of_formula`) REFERENCES `formulas` (`id_of_formula`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `calculations_result`
--

LOCK TABLES `calculations_result` WRITE;
/*!40000 ALTER TABLE `calculations_result` DISABLE KEYS */;
INSERT INTO `calculations_result` VALUES (2,'2019-03-14 00:00:00',14,20,3),(2,'2019-03-14 00:00:00',15,25,3),(2,'2019-04-09 11:45:39',31,3,1),(3,'2019-09-24 13:56:09',47,100,1);
/*!40000 ALTER TABLE `calculations_result` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `elements`
--

DROP TABLE IF EXISTS `elements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `elements` (
  `code` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `short_name` varchar(50) DEFAULT NULL,
  `measure` varchar(20) NOT NULL,
  `rigid` tinyint(1) DEFAULT '0',
  `voc` tinyint(1) DEFAULT '0' COMMENT 'Летучие органические вещества',
  `hydrocarbon` tinyint(1) DEFAULT '0' COMMENT 'Углеводород',
  `formula` varchar(100) DEFAULT '0',
  `cas` varchar(45) DEFAULT '"-"' COMMENT 'Уникальный численный идентификатор веществ',
  PRIMARY KEY (`code`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `elements`
--

LOCK TABLES `elements` WRITE;
/*!40000 ALTER TABLE `elements` DISABLE KEYS */;
INSERT INTO `elements` VALUES (8,'Взвешенные частицы РМ10','Взвешенные частицы РМ10','кг/м3',1,0,0,'-','-'),(10,'Взвешенные частицы РМ2.5','Взвешенные частицы РМ2.5','мг/м3',1,0,0,'-','-'),(101,'диАлюминий триоксид в пересчете на алюминий','диАлюминий триоксид','мг/м3',1,0,0,'Al2O3','1344-28-1'),(102,'Алкилсульфат натрия','Алкилсульфат натрия','мг/м3',1,0,0,'-','-'),(103,'Альфа-3 /действующее начало - кальций дихлорацетат/','Альфа-3','мг/м³',1,0,0,'-','-'),(104,'Барий карбонат /в пересчете на барий/ (Барий углекислый)','Барий карбонат','мг/м³',1,0,0,'СBaО₃','513-77-9'),(106,'Барий оксид /в пересчете на барий/','Барий оксид','мг/м³',1,0,0,'BaO','1304-28-5'),(108,'Барий сульфат /в пересчете на барий/','Барий сульфат','мг/м³',1,0,0,'BaO₄S','7727-43-7'),(109,'Бериллий и его соединения /в пересчете на бериллий/','Бериллий и его соединения','мг/м³',1,0,0,'-','-'),(110,'диВанадий пентоксид (пыль) (Ванадия пятиокись)','диВанадий пентоксид','мг/м³',1,0,0,'O₅V₂','1314-62-1'),(111,'Висмут оксид','Висмут оксид','мг/м³',1,0,0,'Вi₂О₃','1304-76-3'),(112,'диНатрий тетраоксовольфрамат (VI) /в пересчете на вольфрам/ (Вольфрамат натрия)','диНатрий тетраоксовольфрамат (VI)','мг/м³',1,0,0,'Na₂O₄W x H₄O₂','10213-10-2'),(113,'Вольфрам триоксид (Ангидрид вольфрамовый)','Вольфрам триоксид','мг/м³',1,0,0,'O₃W','1314-35-8'),(114,'Германий диоксид /в пересчете на германий/','Германий диоксид','мг/м³',1,0,0,'GeO₂','1310-53-8'),(115,'Магний диборид','Магний диборид','мг/м³',1,0,0,'B₂Mg₃','12397-24-9'),(116,'Титан диборид','Титан диборид','мг/м³',1,0,0,'TiB₂','12045-63-5	'),(117,'Титан хром диборид','Титан хром диборид','мг/м³',1,0,0,'CrTiB₂','39407-17-5	'),(118,'Титан диоксид','Титана диоксид','мг/м³',1,0,0,'O₂Ti','13463-67-7'),(119,'Диэтилртуть /в пересчете на ртуть/','Диэтилртуть','мг/м³',1,0,0,'C₄H₁₀Hg','627-44-1'),(120,'Индий (III) тринитрат /в пересчете на индий/','Индий (III) тринитрат','мг/м³',1,0,0,'InN₃O9','13465-14-0'),(121,'Железо сульфат /в пересчете на железо/','Железо сульфат','мг/м³',1,0,0,'FeO₄S','7720-78-7'),(122,'Железо трихлорид /в пересчете на железо/ (Железа хлорид)','Железа трихлорид','мг/м³',1,0,0,'Cl₃Fe','7705-08-0'),(123,'диЖелезо триоксид /в пересчете на железо/ (Железа оксид)','диЖелезо триоксид (Железа оксид)','мг/м³',1,0,0,'Fе₂О₃, FeO','1309-37-1'),(124,'Кадмий динитрат /в пересчете на кадмий/','Кадмий динитрат	','мг/м³',1,0,0,'CdN₂O₆','10022-68-1'),(125,'диКалий карбонат (Калия карбонат; Поташ)','диКалий карбонат','мг/м³',1,0,0,'СК₂О₃','584-08-7'),(126,'Калий хлорид','Калий хлорид','мг/м³',1,0,0,'ClK','7447-40-7'),(127,'Кальций гипохлорит','Кальций гипохлорит','мг/м³',1,0,0,'СаСl₂O₂','7778-54-3'),(128,'Кальций оксид (Негашеная известь)','Кальций оксид','мг/м³',1,0,0,'СаО','1305-78-8'),(129,'Кальций карбид (Мел)','Кальций карбид','мг/м³',1,0,0,'С₂Са','75-20-7'),(130,'Кадмий дихлорид /в пересчете на кадмий/ (Кадмия хлорид)','Кадмий дихлорид','мг/м³',1,0,0,'CdCl₂','10108-64-2'),(131,'Кадмий дийодид /в пересчете на кадмий/','Кадмий дийодид','мг/м³',1,0,0,'CdI₂','7790-80-9'),(132,'Кадмий сульфат /в пересчете на кадмий/','Кадмий сульфат','мг/м³',1,0,0,'CdO₄S','7790-84-3'),(133,'Кадмий оксид /в пересчете на кадмий/','Кадмий оксид','мг/м³',1,0,0,'CdO','1306-19-0'),(134,'Кобальт (Кобальт металлический)','Кобальт','мг/м³',1,0,0,'Со','7440-48-4'),(135,'	Кобальт сульфат /в пересчете на кобальт/','Кобальт сульфат','мг/м³',1,0,0,'CoO₄S','10026-24-1'),(136,'Литий хлорид /в пересчете на литий/','Литий хлорид','мг/м³',1,0,0,'ClLi','7447-41-8'),(137,'Магний додекаборид (Магний полиборид)','Магний додекаборид','мг/м³',1,0,0,'B₁₂Mg','12230-32-9'),(138,'Магний оксид','Магний оксид','мг/м³',1,0,0,'MgO','1309-48-4'),(139,'Магний дихлорат гидрат (Магния хлорат)','Магния хлорат','мг/м³',1,0,0,'Cl₂MgO₆ x H₂O','10326-21-3'),(140,'Медь сульфат /в пересчете на медь/ (Медь сернокислая)','Медь сульфат','мг/м³',1,0,0,'CuO₄S','18939-64-2'),(141,'Трихлорфенолят меди (Медь (II) трихлорфенолят)','Трихлорфенолят меди','мг/м³',1,0,0,'C₁₂H₄Cl₆CuO₂','25267-55-4'),(142,'Медь дихлорид /в пересчете на медь/ (Медь хлорная)','Медь дихлорид','мг/м³',1,0,0,'CuCl₂','7447-39-4'),(143,'Марганец и его соединения /в пересчете на марганец (IV) оксид/','Марганец и его соединения','мг/м³',1,0,0,'-','-'),(144,'Медь хлорид /в пересчете на медь/','Медь хлорид','мг/м³',1,0,0,'ClCu','7758-89-6'),(145,'Медь сульфит (1:1) /в пересчете на медь/ (Медь сернистая)','Медь сульфит','мг/м³',1,0,0,'СuO₃S','14013-02-6'),(146,'Медь оксид /в пересчете на медь/','Медь оксид','мг/м³',1,0,0,'CuO','1317-38-0'),(147,'Аденозин-5`-(тетрагидротрифосфат динатрия) (Аденозин-5-трифосфорной кислоты динатриевая соль; АТФ)','АТФ','мг/м³',1,0,0,'C₁₀H₁₄N₅NaO₁₃P₃','987-65-5'),(148,'1,3-Дихлор-1,3,5-триазин-2,4,6(1Н,3Н,5Н)трион натрия (Дихлоризоциануровой кислоты натриевая соль)','Дихлоризоциануровой к-ты натриевая соль','мг/м³',1,0,0,'C₃Cl₂N₃NaO₃','2893-78-9'),(149,'2,2-Дихлорпропаноат натрия (Далапон; 2,2-Дихлорпропановой кислоты натриевая соль)','Далапон','мг/м³',1,0,0,'C₃H₃Cl₂NaO₂','127-20-8'),(150,'Натрий гидроксид (Натр едкий; Сода каустическая)','Натрий гидроксид','мг/м³',1,0,0,'HNaO','1310-73-2');
/*!40000 ALTER TABLE `elements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emissions`
--

DROP TABLE IF EXISTS `emissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `emissions` (
  `code` int(11) NOT NULL,
  `poi` int(11) DEFAULT NULL,
  `volume` double unsigned zerofill DEFAULT NULL,
  `enviroment` int(11) DEFAULT NULL,
  `day1` datetime NOT NULL,
  `day2` datetime NOT NULL,
  `device` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`code`),
  KEY `enviroment_idx` (`enviroment`),
  KEY `poi_idx` (`poi`),
  CONSTRAINT `elements` FOREIGN KEY (`code`) REFERENCES `elements` (`code`),
  CONSTRAINT `enviroment` FOREIGN KEY (`enviroment`) REFERENCES `environment` (`id`),
  CONSTRAINT `poi` FOREIGN KEY (`poi`) REFERENCES `poi` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emissions`
--

LOCK TABLES `emissions` WRITE;
/*!40000 ALTER TABLE `emissions` DISABLE KEYS */;
/*!40000 ALTER TABLE `emissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emissions_on_map`
--

DROP TABLE IF EXISTS `emissions_on_map`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `emissions_on_map` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idPoi` int(11) DEFAULT NULL,
  `idElement` int(11) NOT NULL,
  `idEnvironment` int(11) NOT NULL,
  `ValueAvg` float DEFAULT NULL,
  `ValueMax` float DEFAULT NULL,
  `idPoligon` int(11) DEFAULT NULL,
  `Year` int(11) NOT NULL,
  `Month` int(11) DEFAULT NULL,
  `day` int(11) DEFAULT NULL,
  `Measure` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `poiFK_idx` (`idPoi`),
  KEY `elementFK_idx` (`idElement`),
  KEY `environmentFK_idx` (`idEnvironment`),
  KEY `poligonFK_idx` (`idPoligon`),
  CONSTRAINT `elementFK` FOREIGN KEY (`idElement`) REFERENCES `elements` (`code`),
  CONSTRAINT `environmentFK` FOREIGN KEY (`idEnvironment`) REFERENCES `environment` (`id`),
  CONSTRAINT `poiFK` FOREIGN KEY (`idPoi`) REFERENCES `poi` (`id`),
  CONSTRAINT `poligonFK` FOREIGN KEY (`idPoligon`) REFERENCES `poligon` (`id_of_poligon`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emissions_on_map`
--

LOCK TABLES `emissions_on_map` WRITE;
/*!40000 ALTER TABLE `emissions_on_map` DISABLE KEYS */;
INSERT INTO `emissions_on_map` VALUES (2,35,148,4,20,50,NULL,2019,11,21,'мг/м³');
/*!40000 ALTER TABLE `emissions_on_map` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `environment`
--

DROP TABLE IF EXISTS `environment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `environment` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `environment`
--

LOCK TABLES `environment` WRITE;
/*!40000 ALTER TABLE `environment` DISABLE KEYS */;
INSERT INTO `environment` VALUES (1,'Атмосфера'),(5,'Господарський грунт'),(4,'Грунт'),(2,'Питна вода'),(3,'Технічна вода'),(6,'технічна техніка');
/*!40000 ALTER TABLE `environment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event`
--

DROP TABLE IF EXISTS `event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `event` (
  `event_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `lawyer_vefirication` tinyint(4) DEFAULT NULL,
  `dm_verification` tinyint(4) DEFAULT NULL,
  `id_of_user` int(11) DEFAULT NULL,
  `issue_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`event_id`),
  KEY `userid_fk_idx` (`id_of_user`),
  KEY `issue_id_fk_idx` (`issue_id`),
  CONSTRAINT `issue_id_fk` FOREIGN KEY (`issue_id`) REFERENCES `issues` (`issue_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `userid_fk` FOREIGN KEY (`id_of_user`) REFERENCES `user` (`id_of_user`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
INSERT INTO `event` VALUES (1,'Zahid','zag',NULL,NULL,5,12),(2,'test','Опис',0,NULL,1,12),(3,'test2','Опис',NULL,NULL,3,13),(4,'КПІ','Опис: КПІ',NULL,NULL,5,16),(5,'КПІ','Опис1',NULL,NULL,5,16);
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event_documents`
--

DROP TABLE IF EXISTS `event_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `event_documents` (
  `event_id` int(11) NOT NULL,
  `document_code` varchar(100) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`event_id`,`document_code`),
  CONSTRAINT `eventid_fk` FOREIGN KEY (`event_id`) REFERENCES `event` (`event_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event_documents`
--

LOCK TABLES `event_documents` WRITE;
/*!40000 ALTER TABLE `event_documents` DISABLE KEYS */;
INSERT INTO `event_documents` VALUES (2,'d471410',''),(2,'d471411',''),(2,'d471576','kh');
/*!40000 ALTER TABLE `event_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event_resource`
--

DROP TABLE IF EXISTS `event_resource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `event_resource` (
  `resource_id` int(11) NOT NULL,
  `event_id` int(11) NOT NULL,
  `value` int(11) DEFAULT NULL,
  `description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`resource_id`,`event_id`),
  KEY `event_id` (`event_id`),
  CONSTRAINT `event_resource_ibfk_1` FOREIGN KEY (`resource_id`) REFERENCES `resource` (`resource_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `event_resource_ibfk_2` FOREIGN KEY (`event_id`) REFERENCES `event` (`event_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event_resource`
--

LOCK TABLES `event_resource` WRITE;
/*!40000 ALTER TABLE `event_resource` DISABLE KEYS */;
INSERT INTO `event_resource` VALUES (4,1,200,'na lechenie'),(4,2,123,'Гривні'),(4,3,222,'Гривні'),(4,5,12121,'Гривні');
/*!40000 ALTER TABLE `event_resource` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event_template`
--

DROP TABLE IF EXISTS `event_template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `event_template` (
  `template_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `expert_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`template_id`),
  KEY `expert_id_fk_idx` (`expert_id`),
  CONSTRAINT `expert_id_fk` FOREIGN KEY (`expert_id`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event_template`
--

LOCK TABLES `event_template` WRITE;
/*!40000 ALTER TABLE `event_template` DISABLE KEYS */;
INSERT INTO `event_template` VALUES (1,'ликвидация пожара','77',1),(2,'фів','фів',1),(3,'','',1);
/*!40000 ALTER TABLE `event_template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expert`
--

DROP TABLE IF EXISTS `expert`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `expert` (
  `id_of_expert` int(11) NOT NULL,
  `expert_name` varchar(45) DEFAULT NULL,
  `expert_FIO` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_of_expert`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expert`
--

LOCK TABLES `expert` WRITE;
/*!40000 ALTER TABLE `expert` DISABLE KEYS */;
INSERT INTO `expert` VALUES (0,'Адміністратор','Поуляк В.Є.'),(1,'Економіст','Іванов У.І.'),(2,'Еколог','Рокляна Б.З.'),(3,'Медик','Яхзекович Х.З.'),(4,'Юрист','Документач В.І.'),(5,'Аналітик','Кряка К.Р.');
/*!40000 ALTER TABLE `expert` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `formula_compound`
--

DROP TABLE IF EXISTS `formula_compound`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `formula_compound` (
  `id_of_formula` int(11) NOT NULL,
  `id_of_parameter` int(11) NOT NULL,
  `id_of_expert` int(11) NOT NULL,
  PRIMARY KEY (`id_of_formula`,`id_of_parameter`,`id_of_expert`),
  KEY `formula_compound_expert_id_of_expert_fk` (`id_of_expert`),
  KEY `formula_compound_formula_parameters_id_of_parameter_fk_idx` (`id_of_parameter`),
  CONSTRAINT `formula_compound_expert_id_of_expert_fk` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`),
  CONSTRAINT `formula_compound_formula_parameters_id_of_parameter_fk` FOREIGN KEY (`id_of_parameter`) REFERENCES `formulas` (`id_of_formula`),
  CONSTRAINT `formula_compound_formulas_id_of_formula_fk` FOREIGN KEY (`id_of_formula`) REFERENCES `formulas` (`id_of_formula`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formula_compound`
--

LOCK TABLES `formula_compound` WRITE;
/*!40000 ALTER TABLE `formula_compound` DISABLE KEYS */;
INSERT INTO `formula_compound` VALUES (31,32,1),(31,33,1),(31,34,1),(31,35,1),(31,36,1),(37,38,1),(37,39,1),(37,40,1),(41,42,1),(41,43,1),(41,44,1),(41,45,1),(41,46,1),(41,47,1),(41,48,1),(43,62,1),(43,63,1),(43,64,1),(43,65,1),(43,66,1),(43,67,1),(47,143,1),(47,144,1),(48,145,1),(48,146,1),(49,50,1),(49,51,1),(49,52,1),(50,53,1),(50,54,1),(50,55,1),(50,56,1),(50,57,1),(51,58,1),(51,59,1),(52,60,1),(52,61,1),(63,68,1),(63,69,1),(63,70,1),(64,65,1),(64,71,1),(65,73,1),(65,74,1),(66,81,1),(66,82,1),(67,83,1),(67,84,1),(67,85,1),(67,86,1),(67,87,1),(67,88,1),(67,89,1),(67,90,1),(71,72,1),(71,73,1),(75,76,1),(75,77,1),(75,78,1),(75,79,1),(75,80,1),(92,94,1),(92,95,1),(93,95,1),(93,96,1),(93,97,1),(98,99,1),(98,100,1),(102,103,1),(102,104,1),(102,105,1),(106,107,1),(106,108,1),(106,109,1),(110,108,1),(110,111,1),(110,112,1),(110,113,1),(115,116,1),(115,117,1),(115,118,1),(115,119,1),(115,120,1),(115,121,1),(115,122,1),(123,124,1),(123,125,1),(123,126,1),(123,127,1),(123,128,1),(123,129,1),(130,132,1),(130,133,1),(130,134,1),(130,135,1),(130,136,1),(130,137,1),(131,132,1),(131,134,1),(131,135,1),(131,136,1),(131,137,1),(138,139,1),(138,140,1),(141,125,1),(141,126,1),(141,127,1),(141,129,1),(141,139,1),(141,142,1),(145,147,1),(145,148,1),(145,149,1),(145,150,1),(146,151,1),(146,152,1),(153,154,1),(153,155,1),(153,156,1),(157,158,1),(157,159,1),(157,160,1),(161,162,1),(161,163,1),(161,164,1),(161,165,1),(161,166,1),(171,171,2),(1,2,3),(1,3,3),(7,8,3),(7,9,3),(7,10,3),(7,168,3),(13,14,3),(13,15,3),(13,16,3),(13,17,3),(18,19,3),(20,21,3),(20,22,3),(20,23,3),(20,24,3),(25,26,3),(25,27,3),(28,14,3),(28,15,3),(29,27,3),(29,30,3),(167,4,3),(167,5,3),(167,6,3),(169,10,3),(169,11,3),(169,12,3),(169,168,3),(170,19,3);
/*!40000 ALTER TABLE `formula_compound` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `formula_parameters`
--

DROP TABLE IF EXISTS `formula_parameters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `formula_parameters` (
  `id_of_parameter` int(11) NOT NULL AUTO_INCREMENT,
  `name_of_parameter` varchar(45) DEFAULT NULL,
  `measurement_of_parameter` varchar(45) DEFAULT NULL,
  `description_of_parameter` varchar(200) DEFAULT NULL,
  `id_of_expert` int(11) NOT NULL,
  PRIMARY KEY (`id_of_parameter`,`id_of_expert`),
  UNIQUE KEY `id_of_parameter_UNIQUE` (`id_of_parameter`,`id_of_expert`),
  KEY `fk_formula_compound_expert_id_idx` (`id_of_expert`),
  CONSTRAINT `fk_formula_compound_expert_id` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=164 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formula_parameters`
--

LOCK TABLES `formula_parameters` WRITE;
/*!40000 ALTER TABLE `formula_parameters` DISABLE KEYS */;
INSERT INTO `formula_parameters` VALUES (0,'i',NULL,'Індекс для рівнянь з сумою',1),(1,'U(t)','грн','Об`єм накопичення',1),(1,'X1','млн. м^3','об\'єм скидання неочиищенних стічних вод\r\n',3),(2,'C(t)','грн','Об`єм споживання',1),(2,'X2','млн. м^3','викиди в атмосферу повітря оксидів азоту',3),(3,'Z(t)','грн','Витрати на забеспечення безпеки населення',1),(3,'X3','тис. т.','об\'єм скидання забрудненних стічних вод',3),(4,'ЗВ','грн','Запаси та витрати підприємства',1),(4,'X4','тис. т.','об\'єм викидів хімчних речовин',3),(5,'ГКР','грн','Грошові кошти, розрахунки та інші активи підприємства',1),(5,'X5','млн. м^3','об\'єм викидів хімчних речовин',3),(6,'ВМП','грн','Витрати майбутніх періодів підприємства',1),(6,'X1','млн. м^3','об\'єм викидів неочищенних стічних вод',3),(7,'РКП','грн','Розрахунки й інші короткострокові пасиви підприємства',1),(7,'X3','тис. т.','викиди хім. речовин пересувними установками',3),(8,'ЧГПо','грн','Сума чистого грошового потоку підприємства по операційній діяльності',1),(8,'X4','тис. т.','викиди хім. речовин стаціонарними установками',3),(9,'ЧГПі','грн','Сума чистого грошового потоку підприємства по інвестиційній діяльності',1),(9,'X5','тис. т.','викиди хім. речовин стаціонарними установками',3),(10,'ЧГПф','грн','Сума чистого грошового потоку підприємства по фінансовій  діяльності',1),(10,'X1','тис. т.','викиди хім. речовин стаціонарними установками',3),(11,'Mi','т','Обсяг викинутої речовини',1),(11,'X2','тис. т.','викиди хім. речовин пересувними установками',3),(12,'Npi','грн','Ставка податку за тонну викинутої речовини',1),(12,'АДС','мм. рт. ст.','систолічний арт. тиск',3),(13,'ЗДВ','сек.','затримка дихання після гибокого вдиху',3),(14,'СОЗ','','індекс самооцінки здоров\'я',3),(15,'СБ','сек.','статичне балансування',3),(16,'АДП','мм рт.ст','пульсовий тиск',3),(17,'Фв','грн','Збитки від руйнування та пошкодження основних фондів виробничого призначення',1),(17,'МТ','кг','маса тіла',3),(18,'Фг','грн','Збитки від руйнування та пошкодження основних фондів невиробничого призначення',1),(18,'КВ','рік','календарний вік',3),(19,'Пр','грн','Збитки від втрат готової промислової та\nсільськогосподарської продукції',1),(19,'X1','мл. кг^-1 хв^-1','споживання кисню на висоті навантаження',3),(20,'Прс','грн','Збитки від втрат незібраної\nсільськогосподарської продукції',1),(20,'X2','хв^-1','макс. значення частоти серцевих скорочень',3),(21,'Сн','грн','Збитки від втрат запасів сировини,\nнапівфабрикатів та проміжної продукції',1),(21,'X3','Вт','потужність порогового фіз. навантаження',3),(22,'Мдг','грн','Збитки від втрат майна громадян та організацій',1),(22,'X4','ум. од.','мін. значення вентиляційного еквіваленту',3),(23,'ΔP','грн','Зменшення балансової вартості i-го виду основних фондів виробничого призначення внаслідок повного або часткового руйнування з\nурахуванням відповідних коефіцієнтів індексації',1),(25,'Ka','','Kоефіцієнт амортизації i-го виду основних фондів виробничого призначення',1),(28,'Лв','грн','Ліквідаційна вартість одержаних матеріалів і устаткування',1),(30,'Пр(п)','грн','Збитки від втрат готової промислової продукції',1),(31,'Пр(с)','грн','Збитки від втрат готової сільськогосподарської продукції',1),(32,'Сi','грн','Собівартість одиниці i-го виду промислової продукції',1),(33,'qi','од','Кількість втраченої продукції i-го виду',1),(35,'Ці','од','Середня оптова ціна i-го виду сільськогосподарської продукції',1),(36,'Si','м^2','Площа пошкодження i-ї сільськогосподарської культури',1),(37,'кі','','Середній коефіцієнт пошкодження посівів i-ї сільськогосподарської культури',1),(38,'Уі','','Середня очікувана прогностична урожайність i-ї сільськогосподарської\nкультури регіоні',1),(40,'Зі(дод)','грн','Витрати, необхідні для доведення всього обсягу втраченої i-ї сільськогосподарської продукції до товарного вигляду',1),(41,'Цi(сер)','грн','Середня оптова ціна одиниці i-ї сировини, матеріалів та напівфабрикатів на момент виникнення втрат',1),(49,'Р(с/г)1','грн','Збитки від вилучення сільськогосподарських угідь з користування',1),(50,'Р(с/г)2','грн','Збитки від порушення сільськогосподарських угідь',1),(51,'Н','грн/га','норматив збитків для різних видів сільськогосподарських\nугідь по областях',1),(52,'П','га','Площа сільськогосподарських угідь відповідного виду, які вилучаються з користування',1),(54,'k','','Коефіцієнт зниження продуктивності угіддя',1),(55,'Н','грн/га','Норматив збитків для різних видів сільськогосподарських угідь по областях',1),(56,'П','га','Площа сільськогосподарських угідь відповідного виду, які вилучаються з користування',1),(57,'В','грн','Вартість 1 тонни живої ваги постраждалої тварини за середніми цінами, які склалися на підприємстві, що зазнало втрат',1),(58,'N','т','Загальна вага постраждалих тварин',1),(59,'Аф','грн','Збиток від забруднення атмосфери',1),(60,'Вф','грн','Збиток від забруднення поверхневих і\nпідземних вод',1),(61,'Зф','грн','Збиток від забруднення землі і ґрунту ',1),(63,'Нр','грн','Збиток від втрати і життя і здоров`я населення',1),(64,'Мр','грн','Збиток від ушкодження і руйнування ОВФ, майна і споруджень',1),(65,'Ррг','грн','Збиток від втрат у рибному господарстві',1),(66,'Рлг','грн','Збиток від втрати продукції й об`єктів лісового\nгосподарства',1),(67,'Ррек','грн','Збиток від знищення і погіршення якості рекреаційних ресурсів',1),(68,'Рпзф','грн','Збиток, заподіяний природно-заповідному фондові',1),(69,'Рсг','грн','Збиток від відторгнення сільськогосподарських угідь',1),(70,'Втрр','грн','Втрати від вибуття трудових ресурсів з виробництва',1),(71,'Вдп','грн','Витрати на виплату допомоги на поховання',1),(72,'Втг','грн','Витрати на виплату пенсій у разі втрати годувальника',1),(73,'Мл','грн','Втрати від легкого нещасного випадку',1),(74,'Nл','чол','Кількість постраждалих від легкого нещасного випадку',1),(75,'Мт','грн','Втрати від тяжкого нещасного випадку',1),(76,'Nт','чол','Кількість постраждалих від тяжкого нещасного випадку',1),(77,'Мз','грн','Втрати від загибелі людини',1),(78,'Nз','чол','Кількість загиблих людей',1),(79,'Мі','грн','Втрати від отримання людиною інвалідності',1),(80,'Nі','чол','Кількість людей що отримали інвалідність',1),(81,'Мдп','0,15* тис. грн/чол','Допомога на поховання (за даними органів соціального забезпечення)',1),(82,'Nз','чол','Кількість загиблих',1),(83,'Мвтг','грн','Розмір щомісячної пенсії на дитину до досягнення нею повноліття',1),(84,'Вд','рік','Вік дитини',1),(85,'ВВП','грн','Питомий валовий внутрішній продукт',1),(86,'В0','грн/люд*рік','Питомий приріст ВВП',1),(87,'Тт','рік','Початок трудового віку',1),(88,'Тр','рік','Початок пенсійного віку',1),(89,'Еф','1/рік','Норматив дисконтування',1),(90,'Мдг(г)','грн','Збитки від втрат майна громадян',1),(91,'Мдг(о)','грн','Збитки від втрат майна організацій',1),(92,'Pi','грн','Балансова вартість i-го виду втраченого майна організацій',1),(93,'Кі(а)','','Коефіцієнт амортизації i-го виду втраченого майна організацій',1),(94,'кі','','Індекс зміни цін стосовно часу придбання i-го виду майна',1),(95,'qі(орг)','од.','Кількість втраченого майна організацій i-го виду',1),(96,'Ці(с.р)','грн','Середня ринкова ціна і-го виду втраченого майна громадян',1),(97,'qі(гр)','од.','Кількість втраченого майна громадян і-го виду',1),(99,'Р(л/г)1','грн','Збитки від знищення лісу та вилучення земельних ділянок лісового фонду для цілей, не пов`язаних з веденням лісового господарства',1),(100,'Р(л/г)2','грн','Збитки від пошкодження лісів',1),(101,'Р(л/г)3','грн','Збитки від пошкодження лісів у разі переведення лісів у менш цінну групу ',1),(102,'Н','грн/га','Норматив збитків для груп лісів по областях',1),(103,'К','','Коефіцієнт продуктивності лісів за типами лісогосподарських умов областей',1),(104,'П','га','Площа лісової ділянки, що вилучається або знищується',1),(105,'k','','Коефіцієнт зниження продуктивності угіддя',1),(106,'Н','грн/га','Норматив збитків для груп лісів за регіонами України',1),(107,'П','га','Площа лісової ділянки, що зазнала шкідливого впливу',1),(108,'Н1','грн/га','Норматив збитків до шкідливого впливу',1),(109,'Н2','грн/га','Норматив збитків після шкідливого впливу',1),(110,'К','','Коефіцієнт продуктивності лісів за типами лісорослинних умов',1),(111,'П','га','Площа лісової ділянки, що зазнала шкідливого впливу',1),(112,'N','кг','Прямі збитки рибного господарства\n',1),(113,'N1','кг','Збитки від втрати потомства у рибному господарстві',1),(114,'N2','кг','Збитки від загибелі кормових організмів(планктон)',1),(115,'N3','кг','Збитки від загибелі кормових організмів(бентос)',1),(116,'N4','кг','Збитки від втрат нерестовищ',1),(117,'N5','кг','Збитки від втрати потомства',1),(118,'П','од/м^2','Середня кількість загиблої риби',1),(119,'S','м^2','Площа негативного впливу пошкодження, кв. метрів',1),(120,'М','кг','Середня маса дорослої особини',1),(121,'П1','од/м^2','Середня кількість загиблих личинок',1),(122,'К1','%','Коефіцієнт промислового повернення від личинок',1),(123,'П2','од/м^2','Середня кількість загиблої ікри',1),(124,'К2','%','Коефіцієнт промислового повернення від ікри',1),(125,'П','од','Кількість загиблої риби',1),(126,'Z','%','Частка самок',1),(127,'Q','тис. од ікринок','Cередня плодючість самки',1),(128,'С','разів','Кратність нересту',1),(129,'К','%','Коефіцієнт промислового повернення від ікри',1),(130,'М','кг','Середня маса дорослої особини',1),(131,'S','м^2','Площа пошкодження',1),(133,'Н','м','Глибина водойми',1),(134,'П','гр/м^3','Середня концентрація кормових організмів',1),(135,'Р/В','','Коефіцієнт переведення біомаси кормових організмів у продукцію',1),(136,'К1','%','Показник гранично можливого використання\nкормової бази риб',1),(137,'К2','%','Кормовий коефіцієнт для переведення продукції кормових організмів у рибопродукцію',1),(138,'S','м^2','Площа пошкодження',1),(139,'П','гр/м^2','Середня концентрація кормових організмів',1),(140,'Р/В','','Коефіцієнт переведення біомаси кормових організмів у продукцію',1),(141,'К1','%','Показник гранично можливого використання\nкормової бази риб',1),(142,'К2','%','Кормовий коефіцієнт для переведення продукції кормових організмів у рибопродукцію',1),(143,'S','га','Площа пошкодження',1),(144,'Р','кг/га','Середня рибопродуктивність нерестовищ за промисловим поверненням',1),(145,'S','га','Площа пошкодження',1),(146,'П','од/га','Кількість плідників на нерестовищах',1),(148,'Q','тис. од','Середня плодючість самки',1),(149,'С','разів','Кратність нересту',1),(150,'К','%','Коефіцієнт промислового повернення від ікри',1),(151,'М','кг','Середня маса дорослої особини',1),(152,'Т','','Термін, необхідний для відновлення рекреаційної зони',1),(153,'П','','Прибуток у цілому від діяльності установи за одиницю розрахункового терміну на одному об`єкті рекреаційної зони',1),(154,'Зр','грн','Сума збитків об`єктів рекреаційної зони антропогенного впливу',1),(155,'Рп','грн','Витрати на відновлення ресурсів природного\nпоходження',1),(156,'Рс','грн','Витрати на відновлення ресурсів антропогенного\nпоходження',1),(157,'Пз','грн','Сума витрат на відновлення природного стану об`єкта природно-заповідного фонду',1),(158,'Рз','грн','Недоотримані надходження від рекреаційної, наукової, природоохоронної, туристсько-\nекскурсійної та іншої діяльності установи природно-заповідного фонду',1),(159,'Ап','грн','Витрати на експертизу екологічної та ландшафтної структури об`єкта природно-заповідного фонду',1),(160,'Анс','грн','Витрати на експертизу змін стану біогеоценозів об`єкта природно-заповідного фонду, що постраждав внаслідок антропогенного впливу',1),(161,'І','грн','Сума збитків, заподіяних біогеоценозам за\nокремими складовими збитків ',1),(162,'Qi','грн','Прибуток i-ї установи природно-заповідного фонду до антропогенного впливу',1),(163,'Qi(п)','грн','Прибуток i-ї установи природно-заповідного фонду після антропогенного впливу',1);
/*!40000 ALTER TABLE `formula_parameters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `formulas`
--

DROP TABLE IF EXISTS `formulas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `formulas` (
  `id_of_formula` int(11) NOT NULL AUTO_INCREMENT,
  `name_of_formula` varchar(100) DEFAULT NULL,
  `description_of_formula` varchar(200) DEFAULT NULL,
  `id_of_expert` int(11) NOT NULL,
  `measurement_of_formula` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_of_formula`,`id_of_expert`),
  UNIQUE KEY `id_of_formula_UNIQUE` (`id_of_formula`,`id_of_expert`),
  KEY `fk_formulas_expert_id_idx` (`id_of_expert`),
  CONSTRAINT `fk_formulas_expert_id` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=172 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formulas`
--

LOCK TABLES `formulas` WRITE;
/*!40000 ALTER TABLE `formulas` DISABLE KEYS */;
INSERT INTO `formulas` VALUES (1,'ОСТЖ(ч)','Оцінка середньої тривалості життя для чоловіків',3,'років'),(2,'Х1(остж)','Об`єм скидання неочищених стічних вод в поверхневі води',3,'одн'),(3,'Х2(остж)','Викиди в атмосферне повітря оксидів азоту',3,'одн'),(4,'Х3(остж)','Об`єм скидання забруднених стічних вод в поверхневі води',3,'одн'),(5,'Х4(остж)','Об`єм викидів в атмосферне повітря хімічних речовин за один рік',3,'одн'),(6,'Х5(остж)','Об`єм викидів в атмосферне повітря хімічних речовин за другий рік',3,'одн'),(7,'БВ(ж)','Розрахунок біологічного віку для жінок',3,'років'),(8,'АДС','Систолічний артеріальний тиск',3,'одн'),(9,'ЗДВ','Затримка дихання після глибокого вдиху',3,'одн'),(10,'СОЗ','Індекс самооцінки здоров`я',3,'одн'),(11,'АДП','Пульсовий тиск',3,'одн'),(12,'МТ','Маса тіла',3,'одн'),(13,'ФВ ССС(ж)','Розрахунок функціонального віку серцево-судинної системи для жінок',3,'років'),(14,'Х1(фв ссс)','Сподивання кисню на висоті навантаження',3,'одн'),(15,'Х2(фв ссс)','Максимальне значення частоти серцевих скорочень',3,'одн'),(16,'Х3(фв ссс)','Потужність порогового фізичного навантаження',3,'одн'),(17,'Х4(фв ссс)','Мінімальне значення вентиляційного еквіваленту',3,'одн'),(18,'НБВ(ч)','Розрахунок належного біологічного віку для чоловіків',3,'років'),(19,'КВ','Календарний вік',3,'років'),(20,'ГІМ','Прогноз захворювання на гострий інфаркт міокарду',3,'років'),(21,'Х1(гім)','Об`єм скидання недостатньо очищених стічних вод в поверхневі водні об`єкти',3,'одн'),(22,'Х2(гім)','Викиди в атмосферне повітря хімічних речовин пересувними установками',3,'одн'),(23,'Х3(гім)','Викиди в атмосферне повітря хімічних речовин\nстаціонарними установкамии в преший рік',3,'одн'),(24,'Х4(гім)','Викиди в атмосферне повітря хімічних речовин\nстаціонарними установкамии у другий рік',3,'одн'),(25,'ХЦВП','Прогноз захворювання на хронічну цереьровасну патологію',3,'одн'),(26,'Х2(хцвп)','Об`єм скидання недостатньо очищених стічних вод в поверхневі водні об`єкти',3,'одн'),(27,'Х1(хцвп)','Викиди в атмосферне повітря хімічних речовин стацонарними установками',3,'одн'),(28,'ФВ ССС(ч)','Розрахунок функціонального віку серцево-судинної системи для чоловіків',3,'років'),(29,'МІ','Прогноз захворювання на мозковий інсульт',3,'одн'),(30,'Х1(мі)','Об`єм скидання забруднених стічних вод в поверхневі водні об`єкти',3,'одн'),(31,'Рш','Розмір шкоди від забруднення земель',1,'грн'),(32,'Гоз','Нормативна грошова оцінка земельної ділянки,\nщо зазнала забруднення',1,'грн/кв.м'),(33,'Пд','Площа забрудненої земельної ділянки',1,'кв.м'),(34,'Кз','Коефіцієнт забруднення земельної ділянки, що характеризує кількість забруднюючої\nречовини в об`ємі забрудненої землі залежно від глибини просочування',1,'одн'),(35,'Кн','Коефіцієнт небезпечності забруднюючої речовини',1,'одн'),(36,'К','Коефіцієнт еколого-господарського значення земель',1,'одн'),(37,'Сіф','Сума пофакторних зовнішніх витрат',1,'грн'),(38,'Аф','Збиток від забруднення атмосфери',1,'грн'),(39,'Вф','Збиток від забруднення поверхневих і підземних вод',1,'грн'),(40,'Зф','Збиток від забруднення землі і грунту',1,'грн'),(41,'Сіп','Сума пореципієнтних зовнішніх витрат',1,'грн'),(42,'Нр','Збиток від втрати і життя і здоров`я населення',1,'грн'),(43,'Мр','Збиток від ушкодження і руйнування ОВФ, майна і споруджень',1,'грн'),(44,'Рсг','Збиток від відторгнення сільськогосподарських угідь',1,'грн'),(45,'Ррг','Збиток від витрат у рибному господарстві',1,'грн'),(46,'Рлг','Збиток від втрати продукції й об`єктів лісового господарства',1,'грн'),(47,'Ррек','Збиток від знищення і погіршення якості рекреаційних ресурсів',1,'грн'),(48,'Рпзф','Збиток, заподіяний природно-заповідному фондові',1,'грн'),(49,'Нр','Нормативно-правова оцінка збитку від втрати життя та здоров`я населення',1,'грн'),(50,'Втрр','Втрати від видобуття трудових ресурсів з виробництва',1,'грн'),(51,'Вдп','Витрати на виплату допомоги на поховання',1,'грн'),(52,'Ввтг','Витрати на виплату пенсій у разі втрати годувальника',1,'грн'),(53,'Мл','Втрати від легкого нещасного випадку',1,'грн'),(54,'Мт','Втрати від тяжкого нещасного випадку',1,'грн'),(55,'Мі','Втрати від отримання людиною інвалідності',1,'грн'),(56,'Мз','Втрати від загибелі людини',1,'грн'),(57,'N','Кількість постраждалих від конкретного виду нещасного випадку',1,'грн'),(58,'Мдп','Допомога на поховання',1,'грн'),(59,'Nз','Кількість загиблих',1,'одн'),(60,'Мвтг','Розмір щомісячної пенсії на дитину до досягнення нею повноліття',1,'грн'),(61,'Вд','Від дитини',1,'років'),(62,'Фв','Збиток від руйнування та пошкодження основних фондів\nвиробничого призначення',1,'грн'),(63,'Фг','Збитки від руйнування та пошкодження основних фондів невиробничого призначення',1,'грн'),(64,'Пр','Збитки від втрат готової промислової та сільскогосподарської продукції',1,'грн'),(65,'Пр-с','Збитки від втрат незібраної сільськогосподарської продукції',1,'грн'),(66,'Сн','Збитки від втрат запасів сировини, напівфабрикатів та проміжної продукції',1,'грн'),(67,'Мдг','Збитки від втрат майна громадян та організацій',1,'грн'),(68,'⌂Р','Зменшення балансової вартості виду основних фондів вирробничого призначення внаслідок повного\nабо часткового руйнування з урахуванням відповідних коефіцієнтів індексації',1,'грн'),(69,'Ка','Коефіцієнт амортизації основних фондів виробничого призначення',1,'одн'),(70,'Лв','Ліквідаційна вартість одержаних матеріалів і устаткування',1,'одн'),(71,'Пр-п','Збитки від втрат готової промислової продукції',1,'грн'),(72,'C','Собівартість одиниці промислової продукції',1,'грн'),(73,'q','Кількість втраченої продукції і-го виду',1,'грн'),(74,'Цj','Середня оптова ціна і-го видку сільськогосподарської продукції в певному регіоні',1,'грн'),(75,'Прс','Збитки від втрат незібраної сільськогосподарської продукції',1,'грн'),(76,'S(прс)','Площа пошкодження сільськогосподарської культури',1,'грн'),(77,'К(прс)','Середній коефіцієнт пошкодження посівів',1,'грн'),(78,'У(прс)','Середня очікувана прогностична урожайність',1,'грн'),(79,'Ц(прс)','Прогностична середня оптова ціна і-го виду продукції',1,'грн'),(80,'Здод(прс)','Витрати, необхідні для доведення всього обсягу втраченої і-ї продукції до товарного виду',1,'грн'),(81,'Цсер','Середня оптова ціна одиниці і-ї сировини, матеріалів та напівфабрикатів на момент виникнення втрат',1,'грн'),(82,'q(сн)','Обсяг втрачених сировини, матеріалів, напівфабрикатів',1,'грн'),(83,'Р(мдг)','Балансова вартість і-го виду втраченого майна організації',1,'грн'),(84,'Ка(мдг)','Коефіцієнт амортизації і-го виду втраченого майна організації',1,'грн'),(85,'к(мдг)','Індекс зміни цін стосовно часу придбання і-го виду майна',1,'грн'),(86,'q_орг(мдг)','Кількість втраченого майна організацій і-го виду',1,'грн'),(87,'Цс.р.(мдг)','Середня ринкова ціна j-го виду втраченого майна громадян',1,'грн'),(88,'q_гр(мдг)','Кількість втраченого майна громадян j-го виду',1,'грн'),(89,'m(мдг)','Кількість видів майна, втраченого організаціями',1,'грн'),(90,'n(мдг)','Кількість видів майна, втраченого громадянами',1,'грн'),(91,'Рс/г','Розрахунок збитків від вилучення або порушення сільськогосподарськихугідь\nта втрат тваринництва',1,'грн'),(92,'Рс/г1','Розрахунок збитків від вилучення ',1,'грн'),(93,'Рс/г2','Розрахунок збитків від порушення сільськогосподарських угідь та втрат тваринництва ',1,'грн'),(94,'Н(рс/г1)','Норматив збитків (узагальнений вартісний показник розміру заподіяної шкоди) для різних видів\nсільськогосподарських угіль',1,'грн'),(95,'П','Площа сільськогосподарських угідь відповідного виду, які вилучаються з користування, у гектарах',1,'грн'),(96,'Н(рс/г2)','Норматив збитків для різних видів сільськогосподарських угідь по областях',1,'грн'),(97,'k','Коефіцієнт зниження продуктивності угіддя',1,'грн'),(98,'Мтв','Розмір збитків',1,'тис.грн'),(99,'B','вартість 1 тонни живої ваги постраждалої тварини за середніми цінами',1,'грн'),(100,'N(мтв)','Загальна вага постраждалих тварин',1,'грн'),(101,'Рл/г','Розрахунок збитків від втрати деревини та інших лісових ресурсів',1,'грн'),(102,'Рл/г1','Розмір збитків',1,'тис.грн'),(103,'Н(обл)','Норматив збитків для груп лісів по областях',1,'грн'),(104,'К(ліс-обл)','Коефіцієнт продуктивності лісів за типами лісогосподарських умов областей',1,'грн'),(105,'П(ліс-вилуч)','Площа лісової ділянки, що вилучається або знищується',1,'гектар'),(106,'Рл/г2','Розмір збитків',1,'тис.грн'),(107,'Н(регіон)','Норматив збитків для груп лісів за регіонами України',1,'тис.грн'),(108,'П(ліс-шкідлив)','Площа лісової ділянки, що зазнала шкідливого впливу',1,'гектар'),(109,'k(зменш продуктивності)','Коефіцієнт зниження продуктивності угіддя',1,'одн'),(110,'Рл/г3','Розмір збитків',1,'тис.грн'),(111,'Н1(група)','Норматив збитків відповідно для груп, до яких угіддя відносилося  до шкідливого впливу',1,'тис.грн'),(112,'Н2(група)','Норматив збитків відповідно для груп, до яких угіддя відносилося  після шкідливого впливу',1,'тис.грн'),(113,'k(продуктивність)','Коефіцієнт продуктивності лісів за типами лісорослинних умов',1,'одн'),(114,'Рр/г','Розрахунок збитків рибного господарства',1,'грн'),(115,'N(р/г)','Величина збитків у натуральному виразі',1,'кілограмів'),(116,'П(р/г, сер)','Середня кількість загиблої риби',1,'штук/кв. метр'),(117,'П1(р/г)','Середня кількість загиблих личинок',1,'штук/кв. метр'),(118,'П2(р/г)','Середня кількість загиблої ікри',1,'штук/кв. метр'),(119,'S(р/г)','Площа негативного впливу пошкодження',1,'кв. метрів'),(120,'М(л)','Середня маса дорослої людини',1,'кг'),(121,'К1(р/г)','Коефіцієнт промислового повернення від личинок',1,'%'),(122,'К2(р/г)','Коефіцієнт промислового повернення від ікри',1,'%'),(123,'N1(р/г)','Обсяг збитків',1,'кілограмів'),(124,'П(р/г)','Кількість загиблої риби',1,'штук'),(125,'Z','Частка самок ',1,'%'),(126,'Q(сер плод)','Середня плодючість самки',1,'штук'),(127,'С','Кратність нересту',1,'разів'),(128,'К(промисл)','Коефіцієнт промислового повернення від ікри',1,'%'),(129,'М(особ)','Середня маса дорослої особини',1,'кг'),(130,'N(планкт)','Збитки в натуральному виразі',1,'тонн'),(131,'N(бентосу)','Збитки в натуральному виразі',1,'тонн'),(132,'S(пошкодж)','Площа пошкодження',1,'кв. метрів'),(133,'H(вод глиб)','Глибина водойми',1,'метрів'),(134,'П(сер. орг)','Середня концентрація кормових організмів',1,'грамів/куб. метр'),(135,'Р/В','Коефіцієнт переведення біомаси кормових організмів у продукцію',1,'грамів/куб. метр'),(136,'К1(гмвк)','Показник гранично можливого використання кормової бази риб',1,'%'),(137,'К2(гмвк)','Кормовий коефіцієнт для переведення продукції кормових організмів',1,'одн'),(138,'N4','Обсяг збитків',1,'кг'),(139,'S2(пошкодж)','Площа пошкодження у гектарах',1,'гектар'),(140,'P(р.п.)','Середня рибопродуктивність нерестовищ за промисловим поверненням',1,'кг/гектар'),(141,'N5','Обсяг збитків',1,'кг'),(142,'П(плід)','Кількість плідників на нерестовищах',1,'штук/гектар'),(143,'Т','Термін, необхідний для відновлення рекреаційної зони',1,'одн'),(144,'П(рек)','Прибуток у цілому від діяльності установи за одиницю розрахункового терміну на одному об`єкті',1,'грн'),(145,'Пз','Сума витрат на відновлення природного стану об`єкта природного-заповідного фонду',1,'грн'),(146,'Рз','Недотримані надхдження від рекреаційної, науковоїо, природоохоронної та іншої діяльності фонду',1,'грн'),(147,'Ап','Витрати на експертизу екологічної та ландшафтної структури об`єкта природно-заповідного фонду',1,'грн'),(148,'Анс','Витрати на експертизу змін стану біогеоценозів фонду внаслідок антропогенного впливу',1,'грн'),(149,'І','Розмір збитків, заподіяних і-му біогеоценозу за окремими складовими збитків',1,'грн'),(150,'k(біогео)','Кількість типів біогеоценозів',1,'одн'),(151,'Q(прибуток)','Прибуток j-ї установи природно-заповідного фонду до антропогенного впливу',1,'грн'),(152,'Q2(прибуток)','Прибуток j-ї установи природно-заповідного фонду після антропогенного впливу',1,'грн'),(153,'Q(t)','Розрахунок національного доходу',1,'грн'),(154,'U(t)','Об`єм накопичення',1,'грн'),(155,'C(t)','Об`єм споживання',1,'грн'),(156,'Z(t)','Витрати на забезпечення безпеки населення',1,'грн'),(157,'ЧГПп','Чистий грошовий потік',1,'грн'),(158,'ЧГПо','Сума чистого грошового потоку підприємства по операційній діяльності ',1,'грн'),(159,'ЧГПі','Сума чистого грошового потоку підприємства по інвестиційній діяльності',1,'грн'),(160,'ЧГПф','Сума чистого грошового потоку підприємства по фінансовій діяльності',1,'грн'),(161,'Е','Економічна ефективність людської діялюності',1,'грн'),(162,'В0','Питомий ВВП',1,'грн'),(163,'Еф','Норматив дисконтування',1,'грн'),(164,'Тт','Початок трудового віку',1,'грн'),(165,'Тр','Початок пенсійного віку',1,'грн'),(166,'вВ0','Темпи щорічного приросту ВВП',1,'грн'),(167,'ОСТЖ(ж)','Оцінка середньої тривалості життя для жінок',3,'років'),(168,'СБ','Статистичне балансування',3,'років'),(169,'БВ(ч)','Розрахунок біологічного віку для чоловіків',3,'років'),(170,'НБВ(ж)','Розрахунок належного біологічного віку для жінок',3,'років'),(171,'Норм','Нормування',2,'одн');
/*!40000 ALTER TABLE `formulas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gdk`
--

DROP TABLE IF EXISTS `gdk`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `gdk` (
  `code` int(11) NOT NULL,
  `mpc_m_ot` double DEFAULT '0' COMMENT 'Max one-time MPC(максимально разовая ПДК)',
  `mpc_avrg_d` double DEFAULT '0' COMMENT 'Average Daily MPC(ПДК среднесуточная)',
  `danger_class` varchar(4) DEFAULT '1',
  `tsel` double DEFAULT '0' COMMENT 'ОРИЕНТИРОВОЧНО БЕЗОПАСНЫЙ УРОВЕНЬ ВОЗДЕЙСТВИЯ ЗАГРЯЗНЯЮЩЕГО АТМОСФЕРУ ВЕЩЕСТВА(ОБУВ)',
  `environment` int(11) DEFAULT NULL,
  PRIMARY KEY (`code`),
  KEY `environment_idx` (`environment`),
  CONSTRAINT `element` FOREIGN KEY (`code`) REFERENCES `elements` (`code`),
  CONSTRAINT `environment` FOREIGN KEY (`environment`) REFERENCES `environment` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gdk`
--

LOCK TABLES `gdk` WRITE;
/*!40000 ALTER TABLE `gdk` DISABLE KEYS */;
INSERT INTO `gdk` VALUES (10,0.16,0.035,'1',0,1),(101,0,0.01,'2',0,1),(102,66,66,'4',0,1);
/*!40000 ALTER TABLE `gdk` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issues`
--

DROP TABLE IF EXISTS `issues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `issues` (
  `issue_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `description` varchar(500) DEFAULT NULL,
  `creation_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`issue_id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issues`
--

LOCK TABLES `issues` WRITE;
/*!40000 ALTER TABLE `issues` DISABLE KEYS */;
INSERT INTO `issues` VALUES (12,'Княжичі','Забруднення водоймища у с. Княжичі','2018-04-19 08:42:12'),(13,'test','testdesc','2018-04-20 08:42:12'),(14,'TEST SREDA','123123123','2019-11-20 08:43:07'),(16,'KPI','KPI','2019-11-21 12:09:31');
/*!40000 ALTER TABLE `issues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issues_documents`
--

DROP TABLE IF EXISTS `issues_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `issues_documents` (
  `issue_id` int(11) NOT NULL,
  `document_code` varchar(100) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`issue_id`,`document_code`),
  CONSTRAINT `issueid_FK` FOREIGN KEY (`issue_id`) REFERENCES `issues` (`issue_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issues_documents`
--

LOCK TABLES `issues_documents` WRITE;
/*!40000 ALTER TABLE `issues_documents` DISABLE KEYS */;
INSERT INTO `issues_documents` VALUES (12,'d471410','tretret'),(13,'d471411','hjhj');
/*!40000 ALTER TABLE `issues_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `parameters_value`
--

DROP TABLE IF EXISTS `parameters_value`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `parameters_value` (
  `calculation_number` int(11) NOT NULL,
  `id_of_parameter` int(11) NOT NULL,
  `parameter_value` double DEFAULT NULL,
  `index_of_parameter` int(11) NOT NULL,
  `id_of_expert` int(11) NOT NULL,
  `id_of_formula` int(11) NOT NULL,
  PRIMARY KEY (`calculation_number`,`id_of_parameter`,`id_of_expert`,`id_of_formula`,`index_of_parameter`),
  KEY `fk_paramater2_id_idx` (`id_of_parameter`),
  KEY `fk_id_of_exp_par_val_idx` (`id_of_expert`),
  KEY `fk_id_of_formula_formulas_idx` (`id_of_formula`),
  CONSTRAINT `fk_id_of_exp_par_val` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_id_of_form_formulas_parameter_value` FOREIGN KEY (`id_of_formula`) REFERENCES `formulas` (`id_of_formula`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_paramater2_id` FOREIGN KEY (`id_of_parameter`) REFERENCES `formulas` (`id_of_formula`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `parameters_value`
--

LOCK TABLES `parameters_value` WRITE;
/*!40000 ALTER TABLE `parameters_value` DISABLE KEYS */;
INSERT INTO `parameters_value` VALUES (2,32,500000,0,1,31),(2,33,3500,0,1,31),(2,34,5,0,1,31),(2,35,4,0,1,31),(2,36,3,0,1,31),(3,143,10,0,1,47),(3,144,10,0,1,47);
/*!40000 ALTER TABLE `parameters_value` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `poi`
--

DROP TABLE IF EXISTS `poi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `poi` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Type` int(11) NOT NULL,
  `Coord_Lat` double NOT NULL,
  `Coord_Lng` double NOT NULL,
  `Description` varchar(100) NOT NULL,
  `Name_Object` varchar(200) NOT NULL,
  PRIMARY KEY (`Id`,`Type`),
  KEY `Type_fk_idx` (`Type`),
  CONSTRAINT `Type_fk` FOREIGN KEY (`Type`) REFERENCES `type_of_object` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `poi`
--

LOCK TABLES `poi` WRITE;
/*!40000 ALTER TABLE `poi` DISABLE KEYS */;
INSERT INTO `poi` VALUES (26,'3',1,50.8614441105892,31.7559814453125,'Публічне акціонерне товариство','dsfbi hsidfh fsdhf hsodhf oshdof hsodhf oshdof hosdhf ohsdof hosdhf ohsdof hosdhf osdhof hsdofh osdhf oshdof hsodfh osdhf oshdof hsdofh oshdf ohsdof hfsodhf oshof hsodfh osdh foshdof '),(27,'1',13,50.9999288558596,29.50927734375,'Товариство з додатковою відповідальністю','єксперемент 1'),(28,'1',12,51.9578073887155,32.05810546875,'Дочірнє підприємство закритого акціонерного товариства','єксперемент 2'),(29,'2',11,50.2015172786048,34.2333984375,'Приватне акціонерне товариство','єксперемент 2'),(30,'2',10,49.5002421645377,34.0631103515625,'Комунальна','єксперемент 2'),(31,'1',4,51.6997998497419,33.7939453125,'Комунальна','qwe'),(32,'0',1,50.4782642355376,30.4056930541992,'Публічне акціонерне товариство','123'),(33,'0',4,50.5064398321055,29.9803161621094,'Публічне акціонерне товариство','123'),(34,'0',4,41.9002327684202,12.4900817871094,'Публічне акціонерне товариство','рим'),(35,'5',1,50.6790567682854,30.2220153808594,'Публічне акціонерне товариство','КПІ');
/*!40000 ALTER TABLE `poi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `point_poligon`
--

DROP TABLE IF EXISTS `point_poligon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `point_poligon` (
  `longitude` double NOT NULL,
  `latitude` double NOT NULL,
  `Id_of_poligon` int(11) NOT NULL,
  `order123` int(11) NOT NULL,
  PRIMARY KEY (`longitude`,`latitude`,`Id_of_poligon`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `point_poligon`
--

LOCK TABLES `point_poligon` WRITE;
/*!40000 ALTER TABLE `point_poligon` DISABLE KEYS */;
INSERT INTO `point_poligon` VALUES (44.9492492666115,27.3779296875,3,3),(47.2643200802548,31.6845703125,3,2),(48.5457054918474,27.66357421875,7,3),(49.7351314162483,32.5140380859375,2,3),(49.7670740736679,34.1455078125,2,2),(50.0077390146369,33.85986328125,3,4),(50.1874507432253,29.970703125,9,3),(50.2612538275847,28.575439453125,9,4),(50.3454604086048,34.4091796875,7,4),(50.4155187040268,37.02392578125,8,1),(50.4470111823122,33.85986328125,2,1),(50.6355263641738,30.904541015625,9,2),(50.7086344008282,29.68505859375,3,1),(50.7607847327148,30.3662109375,6,2),(50.8718447710243,31.1846923828125,6,3),(50.9134242113213,31.146240234375,9,1),(51.0690166596039,30.87158203125,4,2),(51.1655665983618,30.618896484375,6,1),(51.1655665983618,39.39697265625,7,5),(51.2756624341585,41.59423828125,8,2),(51.63165734945,33.6181640625,4,3),(52.3755991766591,30.9375,4,1),(52.6297288671836,25.5322265625,7,2),(53.1072166918934,36.5625,7,1),(53.357108745696,43.59375,8,3);
/*!40000 ALTER TABLE `point_poligon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `poligon`
--

DROP TABLE IF EXISTS `poligon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `poligon` (
  `Id_of_poligon` int(11) NOT NULL,
  `brush_color_r` smallint(3) DEFAULT '200',
  `bruch_color_g` smallint(3) DEFAULT '200',
  `brush_color_b` smallint(3) DEFAULT '200',
  `brush_alfa` smallint(3) DEFAULT '50',
  `line_collor_r` smallint(3) DEFAULT '255',
  `line_color_g` smallint(3) DEFAULT '255',
  `line_color_b` smallint(3) DEFAULT '255',
  `line_alfa` smallint(3) DEFAULT '100',
  `line_thickness` smallint(3) DEFAULT '1',
  `name` varchar(45) DEFAULT NULL,
  `id_of_expert` int(11) NOT NULL,
  `type` varchar(15) NOT NULL DEFAULT 'poligon',
  `description` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`Id_of_poligon`,`id_of_expert`),
  KEY `fk_id_of_exp_poligon_idx` (`id_of_expert`),
  CONSTRAINT `fk_id_of_exp_poligon` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `poligon`
--

LOCK TABLES `poligon` WRITE;
/*!40000 ALTER TABLE `poligon` DISABLE KEYS */;
INSERT INTO `poligon` VALUES (2,0,255,255,250,0,250,2,21,2,'такс',3,'poligon',NULL),(3,0,0,0,250,0,250,2,21,2,'Полiгон опису не мав',1,'poligon',NULL),(4,0,0,0,250,0,250,2,21,2,'Полігон без назви',1,'poligon','Полiгон опису не мав'),(6,0,0,0,250,0,250,2,21,2,'Полігон без назви',1,'poligon','Полiгон опису не мав'),(7,0,0,0,250,0,250,2,21,2,'Полігон без назви',1,'poligon','Полiгон опису не мав'),(8,0,0,0,250,0,250,2,21,2,'',1,'tube','Водопровід опису немав'),(9,0,0,0,250,0,250,2,21,2,'123',5,'tube','Водопровід опису немав');
/*!40000 ALTER TABLE `poligon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `poligon_calculations_description`
--

DROP TABLE IF EXISTS `poligon_calculations_description`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `poligon_calculations_description` (
  `id_poligon` int(11) NOT NULL,
  `calculations_description_number` int(11) NOT NULL,
  `id_of_formula` int(11) DEFAULT '0',
  PRIMARY KEY (`id_poligon`,`calculations_description_number`),
  KEY `FK_p_to_calculations_idx` (`calculations_description_number`),
  CONSTRAINT `FK_c_to_poligon` FOREIGN KEY (`calculations_description_number`) REFERENCES `calculations_description` (`calculation_number`),
  CONSTRAINT `FK_p_to_calculation` FOREIGN KEY (`id_poligon`) REFERENCES `poligon` (`id_of_poligon`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `poligon_calculations_description`
--

LOCK TABLES `poligon_calculations_description` WRITE;
/*!40000 ALTER TABLE `poligon_calculations_description` DISABLE KEYS */;
INSERT INTO `poligon_calculations_description` VALUES (2,2,15),(3,2,31),(4,3,47),(6,3,0),(7,2,31);
/*!40000 ALTER TABLE `poligon_calculations_description` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resource`
--

DROP TABLE IF EXISTS `resource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `resource` (
  `resource_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(200) DEFAULT NULL,
  `units` varchar(100) DEFAULT NULL,
  `price` double DEFAULT NULL,
  PRIMARY KEY (`resource_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resource`
--

LOCK TABLES `resource` WRITE;
/*!40000 ALTER TABLE `resource` DISABLE KEYS */;
INSERT INTO `resource` VALUES (4,'Гроші','Гривні','грн',1);
/*!40000 ALTER TABLE `resource` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template_resource`
--

DROP TABLE IF EXISTS `template_resource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `template_resource` (
  `resource_id` int(11) NOT NULL,
  `template_id` int(11) NOT NULL,
  PRIMARY KEY (`resource_id`,`template_id`),
  KEY `template_resource_ibfk_2_idx` (`template_id`),
  CONSTRAINT `template_resource_ibfk_1` FOREIGN KEY (`resource_id`) REFERENCES `resource` (`resource_id`),
  CONSTRAINT `template_resource_ibfk_2` FOREIGN KEY (`template_id`) REFERENCES `event_template` (`template_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template_resource`
--

LOCK TABLES `template_resource` WRITE;
/*!40000 ALTER TABLE `template_resource` DISABLE KEYS */;
/*!40000 ALTER TABLE `template_resource` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_of_object`
--

DROP TABLE IF EXISTS `type_of_object`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `type_of_object` (
  `Id` int(11) NOT NULL,
  `Name` varchar(200) NOT NULL,
  `Image` longblob,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_of_object`
--

LOCK TABLES `type_of_object` WRITE;
/*!40000 ALTER TABLE `type_of_object` DISABLE KEYS */;
INSERT INTO `type_of_object` VALUES (1,'Виробництво електроенергії',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0aIDAThC͚ml\�\�ߝ\�q\��%\"/`;Iqq!&%%�B�\n��V D[\��PU�6�|(A����\"�\0iy�T*�)Bj�CݚhB\"En\�D\�$nR0N\�ر}�/w\�{�>�\�1޽\�=�\�vҟ�hg�f��\�\�\�\�\�\�%(�\�\�&\�\Z\�\�\�2j>%�S��>�Z\�\� �sM�\�\�G\���T�r���S^n�`\�J\�a\�<\�d`:�@=C�\��y�r����?S\�)����h�z5�#�d	�*\r��\�\�����#\�oh\���-�{�\nS�^�����~����\Z\ZR���+�s�RW_m�?)�\�us�\�J>\�%�t\��Z[��i�\�Jy<�X�zF7=5,\\B=MeteS^�R[�*�N\�� Tj\�F�1�^\��Â�PIKE\�\�/(�\�薦&�\�s!\�&\�6�a�[�KS۷d\�b,�����>xzX�\�ꌍ)\�\�hoÔL���;�a!uL�\�\��+\n\�\�\��b\�\�@X������T[見\�\�\�\�N�9�_ꫵҺl<D�5���\�v\�\�*X�O8\�;2�C���\�\\=�qv�B�3XZse\�\Z`\�6���5ϚI�2N\�(�q��\�v\�\�g8�h\������r*J\�-)A�/wٴK���>�c\�4��0�^�\�wKp\�:3IGGb��\�٩��\�\�ŋ�4\� CQ�<,Y8�Mg!��\�;:c\�1�yY��rfR7\Z�\'X��\�<��*\�\\;�B:g����\�\�C_�\"\�N�!\ZO\�\��\��7]\�m55�ܣ36ΰ|�����}\�	\r\r�\�\nA��o�O��G#1�]\�atc�젎,e\�ՋA\�ks7\�U�4\�LZ�[���\�v\�\Z&p[�\�\ndP�p\���,\�~\�N1�\�\�fS.#\�L�sK�\�\�]ٌC{2�	�D\�p�d�D�H�\�<.>P]�39\�\"9\�\�\�v#�L\�xg\'>\��!z\�G����\�_y+�\�-\�\�\���\�v`��(3\�Y��\�^�J���\�\�}֏_�`��6Η:�V*w�$wm�ԈA\�\'\�\�\�u\�N��G\�V7\��:�8\�Ǿ�U��g��\��\�\�0�r�$�\�L\�̴1a_b\�\�^�iȫ\�$A.�\�\�\�FZ\�\�\�S]T�ty-J�8\'Sil{�\����&)�jE\�|��z�|�n���\�_�lљ\\��\'癱Pg�\\�\�\�ǵ\�݀e+\�sB�ϋ�\�\�O\�b\"�\�h�pm\'{�ϣ�p\�3^��\�f�p$$9	\\x\��0����\��\�\�T�F�/�\��Ǳ��q�w\��\Z\�\�\n\�߯3���>\�u�	yu̅;\�؆u�x^?v�ދޡ���/\�?����\�w�τ�\�\�2?V��C�$��=Hf�X�\��~�.\�ȿene\�y3�˗W.��gW!	s)��U\�B�\�:e2�q�(�FӋ=X���yR\�B��r}_}\�\�*��WW\�\�\�Y\�\�7\�\�v\��܂��p\"`\�ȺpW��\�3\�ܙ\"=�=\n1w�:\�\�\'�T�q*�\�ݏǚg���*�ƫ9��?(\�b�\�-l�\r��Gx�[\�uO�\�\�7*�\�+�\���\�����D\� �;�w\�Y�.\��0(��\�󶗅b�]�V\�훫qlu92�%CY3�\�7{\��h�\Z�\�(O�\�\�|BmdX�\�;\�\�3W8\�}����|��yP�R�S����&Q9^�)+�\�؋:mB�?�\�/\�G#T��5	OΧ�Ѕ.��Ӗd�1`�\�&�G3wِ\�n��t�\�\�\�=f��K�W�wX\�\�Y�.�z�%چ;,TJ�GW��zB[�\Z�	U\��\�\�P���J6�\�b	0\�ڬ�-V��\Z��+\�\'�\�`\�\�Rs9\�g)�K\�t`\�2\�l�%\���*�\���\Z���:7K0\�m����\�$˘��n�`���f\"yب\�\�.����b��7�0x5\�iil��U\�\�芆\r4P=��騕Z�\�\�-lh5�E|�r�\":��\�P!m �\�\n6|7�o��P�\�\�\Z�?t;�\�\�Sj�h\�\�jR^�^\�?�@C���\�9j��7a�\�\�-\�v*�;pQ\0��W\�u\�\�\�\0\0\0\0IEND�B`�'),(2,'Розведення великої рогатої худоби молочних порід',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0QIDAThC\�ipNWǟ!�\�d�\"!�E�b�I�!-e�3\r:\�L�Ԗ2��)�McSU>tP��3��\nU��$*4!��$d�E\"��s\�y�.��ٓ�\���\�s\�9\��ܳ\�s_z�\�D�\�#z\�H\�ɗ�\��x\���[����\�>`\��\Z�ۀ�0\�7���V�P{��c`�0\�{�\�\�\��渫�@\�\�\�t��&w��pcTF	\ZE>����DMT����*+�rr\�0\�W>Gyr���Y+p�knn�m\��XNcuuL��\�Μa,*�1u2\�\��\Z|ԃ��bl�n\�*+E$�P~>c��J[Zr�뎅\�+@�Nc-���%%	�\�о}J�ZN���@�N#-#G2VP <�@�/+m\��=s�E]��	(�\���\�{���b���r!6V߾�|�� q\�T���l\�.��%N�t��?A\n4#=�1OO���\"$}\�\�J��2���=.\�u]�\�gA�lJ`F7��Og�\�KMU���!B�Զ\�\�E$\�%\�a\�߹S\�\�\�\�Pii);r�0%___\Z\�\�A���dBw23i\�P:��[�~QM_m\�F+��SEEYa�|w\��:u\nE\�Đ��%6\�.ee�m�h�\����Cr\0\�e=\�\�K\�\�F߻G��YCE�Et��i���K)\�)���Lw\�fӉ\�\��	�N��];vPXx8]O�F\�o���)�jj�ѣ\�izH��`�kxb�+W�#=աK.�fBzk9�#++��\�\�?���\0\rwr�\�\Zy��\�ǋ\�\�_/Т\�K(�\�o4A\\8w�~OL�\�\�V\n�7�V�^M���\�5I~~\"�P�H\�\�i��1c�w@`\0U\��d\�R�����Y\�r��\�HBݺy�v}GWRR(3##WFuuuR=.��\0�>#���y_�\'2\n�bT�\�	��h?\�rrq�M[�\��%�鋍�k\�Sԧ\�\�8lM�4�\�\�\�)\��}jni�8\�\�Y���z�A&�7�V�QZ�\�\�r�0�僄&@��Bp�Qss3l\r$\�#\�	z\'�\��\�|*))�\�c.\�X0��\Z\�jk\�h\���R���7c�\�SEO��\�CG|-Hä	=����!z�U9d����\Z\�\�?0PZ�>�,�#�(r\�B\Z�9\�\�\�A\�\�\�\�=*�\�ӧ\"��&�O\ZgD�%�}<���xns.\���\�\�\�M�X�/_��\�\�F233\�-k*.�{qnX6�V���\�<��\�W��ᵳ�Q/סCDk׊����\�\r�\�jz��˔B0�\�\�\��:��\�\�Z\�+�\�͇��\�0rqv��~��,j�\�i��y��ڔ�=!GG{�\�.]��\�xF`�H�:%2�<\�J=\�\��m,��\�!\�i3,(�ri�êQ>��^���-�\�\�_9k ��iidaaA�\�ϚЫvm���\�3\�⊊N�\�[J� \\�Eb��)6VZm--����A\n�=\�,m5-\�+_#w��\�U��T+D\n_\�Z��q,`�V�f\�8(��=\�\��r�E\�_����\���\�C\�\�n��\�\r�}�\�H&>�k\�u\�(/\�+,,%�g\��3Gݶ��\�\�a�\�J�a��Fx\�23��U�����g�p�\�M\��\�Όᜇ�\�\�QVc\�\�1fn�nK|�\�Ko)�/?\�\�fC��\r\'�6\r\�\ZoiC\��\�x��8)�(+�w\'��yu!\�h�\'\�O�i\�Z��Q�B�бPy�A\�\�P��	�M�]Ѹ�\�2F�\�\�h.\�\�^\�ho��k����:���\�`�p\�u��=\�?8�\�\r�W\����Ԍ���\0�\�\��`�����uU06T	\��.a���!�\��tǤ\�A�w\�1:��C	p\��Fp�î���D�\�;�	\�\�N;�\Z�Ӷg� [8\�\���i�	N\�A��=���m\�o�������vQt$\�\r���t\�;AT��@��n�O�,q�\�\�\�]�F�㧟Q|�7@D�c.ђ�_�e\0\0\0\0IEND�B`�'),(3,'Трубопровідний транспорт',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0IDAThC\�[le\�O۵7,�m�\��T(A�E\rB�H��(� <_��>�>�D��\'%�K4\�L�ʵ�\�r�KK\��\�\�\��\�\�\�tf��\�n\�_�\�w\���s���nSzH�\�4l�(\�\�	(٨�{Pt:���=}��8�:��\�\'\�85\� \n���\�!���\�Ah��\�=`�ih7\��9U\�@�:�Z\nը\�ޔ؜�n\�ނ�S����*�՝+~g1:��d+\��Gt4QVQf&Q��DMMDW�UVb�5��\�r\�(�{��\�~��	�7nd�t��\�=vP[˼msA�{���\\u4L��ЎNef2o\�\�\�ܬ�AI	sN��=S\�P��\�4�\�\�ɮ�<\�\�j�\Z\"\�\�\�\�vM}�a���h-L1\�Ԩ��d�Ӷ�6h��\���\�\�1;��\�\rnjn\�ںF��U�\�\�d\��S�k8v��l�\�\�А\�üs�\�\�wy\�\�=^�\�|\�[�$��\�~L\�f��a�\�*W\"\�h�,̜I��7\�\�\�N�K*\���J*����\�eQ\\l��oݑ\�\�ZVdyzk\��[��ؗ���+��da�n��s�`P[\�DW\�RpJO�-�@1\�\���m�iF\�\�wh��d�\�z�KD\�<m$�z�d-���\�S��-\�k/f��	��8zLׂ߁\� \0a\�¤I~\�\�s�\�\\��\Z�B3\'ђ�)S4\� �\'k��y�!.F��\'�\�S\�\Z��8\�\�!�\�i��\rӌ�Hb����vR\�\�\�֜�\�e\��1�Ii\'\�\�Kⲍ�;--�q�_�Gs\�\�\�-�=\��\�YimՌ+x\�:���\�\�m\�\�Y\�,X�n?G\�Nޤ7��Ad�ّ������iъZ�\�\rEpU�#xrשS�qR]\�@G~���i	��K�Mm]�~�JJoQ\�\�r�r\�\�Y\�8��j$\�{H5�\��\�E.gC�j�ICc�\\���h�\�I�^\Z�%�PW^�G�wM��-�W���U��6lЂy\�e�\�4\�,\�6v\�\"**�w�݀�\�Ѽ|�uRR�\�g\�\���g�p\���\�r�5�x\�/~� /x�g�{��\�V\�\�B�/S�\Z�m\�\'�PWG�z�L\�Z�\�X�\��e�59q����i�n��\�ػ�h\�>-8�3��\01�\�\�{\�d+�&]f�lw\'OLM�\�\\�\�gQ|\\\r�,~&׻\�\Z��-_�W�#�Y��\�lf�bc�w\�\�g&.0�\�nߔ\��O\�\�\�خ�(\�k\�[��3:Ĝ�\�n\��7\0h �7*,ܕ��h9¼p�q�u�eJ\�ȋ5�e�m�\�(u�\��q\�\�\�\'\Z>;:�t\�/c\�\��)\�mi�6\�D�Y\0\��+�ۯ��\�+X��\�=h�\�ҹ/tr�G��\��.�i\�\�u<\�4���HI>p:\�BP�\�>��H�zV݅:�B\�~M\rF恵���HM�S�\�{gH�\�Oý!��\��\�20�\�V\�=��y�\�\�F�Z��D�@�/k\�\�\�G\�\�:\�?�&p�\�\�0ɗ�yj&r�I4T�N��|�\�妏�3�4g\�y0�L��p:�w\�- �vB���D\n8\r�G\�oP�6�? ��п\Z�UU\�XmvA y�l���ɢ\�\�>  yܗ�;\�,�~�@`��Z|\�\0\���?��\�p�\0\0\0\0IEND�B`�'),(4,'Забір, очищення та постачання води',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0[IDAThC\�}LUeǿ\�MR�ei�\��\�X�9���fN[i��f��Z�m����Z��-+\�\�^\���WZӊ)^3L!h`%����\\�\�\�}�>\���\�9��j\�g�\��=\�<\���\��s.�\�\�N)\�eq\ru���R\�S\�\�\�Tu�\�\�\�X�>L*��B}I��Tu�J��W��\�\'Qۨ&\�-��\�K��=�\�©רz\�-`w\�B=)!���>��V�Ҿ�J�s�FR\�)7\�T&\�n����a�	u�q�=��ٍ7Ӧ�\\\�\�UU@~>�a�YL\�v�;�\�\�1�D��ZS\�\�J%%)UQ�:e�z�\�\�\��\����\�1lxUg�0�~���7O��\'%�9���TX�\�gPzu� i�\�����>}�z\�=�f���\\�N(,\�,\�b��̓\'��4J\'�l�D�����rss�\�t[�\�#�I\�O\����aP�g+H;\�\�\�b�\�޽[,��\�娼]�R��ǯ[SŒ���I\�\�~�СJ���g\'G�K7+U˗/7사�\��\��qF\�����8v\�IZ��g>\�\\�y��5L�χ\�\�D\�neĈb\�\�\�l\��dG\�gVS�Iw=���z\�\�\�\Z\�ϲj��̑28 ��/,�0k�\�\�\�\rs\�ƍ�<y2\"##���@ �]y���)�&?F�]�Y\�LĤ[ga�׋q\�\�a\�رҚ�>\r\�\�\0Ǐ\�yL,8��\�0\�\�ޠ��R��\���~?�b@�\�+�i\�2�m\�bU\�M3\�=�j�:\�ꢻ�Ԅ\'&��3\�U\�\�X\�\�ب����6.�\���2n^k�^@�3MW����K��\�I\��h��>��g��9ۼX����mA\�\�yؼ4\r�#pG��\�7=r�xhØ1b8\�O\�\�ͳ	FIi\':Z�\�f\�߽\0���#���\�=\r\�\�=ؙ��\�\Z�M��}L}z:j\�\�1�}Q�ze\�l\��nVF�\��y�-A�\�\�\����\�I~�;&�9�9\r\n\�ܹ\0\'N���\�\�4M\�M(>R\"5�\�@%\�Nՠ坥(ٺ	\��8&g�\�t^Fn�	6Ji\�\�)1��A�А�ZQ��oǫW\�\�\�_6�{8�����]\�ռ\�Î-Z�\�\�+9�8�Z\�n�\0բ�\�S���\�B1L\�\�\�Gv\��H4\�v@\�\0rid�\�\�f�V��q\�Gw \�W���?a���e	�+�+��c�5�T����Ϛ�\�\�\�\�t4\\\�\�,˔��W#s߷�<Q)GxC\�<�))+�0\��;1\�b>�Y\�\�Áv�s2\�\0�g�\�\�\�paFF�T��%����^�\��/\��FG\�~���0���`\�5\�-\�X�\�|\�M6\�U��h8\�`\���1/O*~`NF��\�AM��v��\�ল\�q|[��\�J+K\�\�\�\\��$nY��#\�ֺǗ�i\�b\�k\���Tpw0�&�SJ;\�\�@V{�	���c2\�\�T֛O\�\\w-\�_L3����������a�Iz.|j���N�;�߭I�p�� r�eh\n�ɮ���@Ֆ\�p��E��ur\��wLj�\����I\�A��8�1u�n\�=!\�瀷ߖ�+O1�7Ķ\�Sn{���o\�\�㾦�^�W^1_\�|�\�{0� ځ\'�R\�\�\�]QQJ}�D\�\"?��Ԝ9\��\�\�\�\�6��\�E\�2E�\�T�|>ɢ\r\�\�Jy�J͝�TD���|��e\�65l�\�\�\�9Y\�\�\�f\�Ǜ�\��\\Q��:d\�T�g��H�dBL�\�b=\��+{K���	\�9l|����9\�\r%R�\�\�^�\�\�7�g\�\��\��\"�eK\\7���\�%.�݄?fq\�*�8��	/�P�\�,\�\�\�\�t	\�}\�\�*\��cRϴRB�:{���j��A�:\�d	\�\�Y{��\rt�g�\��\'Z,.C����\�\�\Z�+\�rh��-�@ug�\�G\�/�\�O�K�Ǩ�?\�\Z|ݒ@Gһ\�\�\�i)�ڒ���\�G��\��Ԓ�Uz��\���\�D��m\�L\r�&?L\�z\'\�Q���t&��J\r�C�<�\\����?�j}$<��\0\0\0\0IEND�B`�'),(5,'Виробництво порожнистого скла',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0�IDAThC\�{PUE\���D�H蘥�Z���X�Y#N�cc\�h5�ͨ��Mcd�Y���J>\"fL\��\Z\�\�� ߯DQIQ|@\"�^y\�\���\��<羸�����\�\�oϹ�~\�ow�{.=�K|��\�\\��\�/\�jW\�b涑\�TTY\�U\�o,��	A\�6�\0cq�0-��*a;a�=\�J�p�\'x$\�^A�1,\�\�m�bO�V@\�f\�\�n	�0�5l\�SaZ��́\�|\�:ƥ@�{\�rX�hh;\�`�!r�t�q\Z\r���b-��\�1������\�\�\�a�\�}�\r\Z�EEQ\�a46����4]�BͰ\�3g�n\�j\�\�+����ɕҵES Ľ�\"�9; *��\'M\"\�\�P�\�15\�\�dH]\�L(�|\"���@�Gq\0��h��\�t|�\�\�\�N^@�����\�O#o+�@kr\nP�cBSSȌ�Ǆ���\�\�\��h�Q\�fl\"z\�Q���-,\�ɗ�\r�JLT<c�a�R\�t1.案��s�۷)�c\�Uj���\�@~�\'+��\�X1\�YGc�c�Q�n�55\n\�%Tm�\�\�\�	\�̳�р/.�\�\�+\�XwM��Oh_�M&\�N��\��={�\�\�wOE�<\��@D\�S��g��#b\�z\�\�5Rva5o���\"\n�\�c;@��=_�v�>��7hֶB�\�B\\�P\��\�Q�7\�QWA\�\\�X�\�p\�Y\�\�q�K\�WJa����\�2�W\r�P0�ݪõvd\�\�	�k\n�\�%~B�ou�h��o���\0\�0w��C\�J2�\\�x6�A\�\�\\\�	q�C\�]�\nS\�n=RAzJ���\nJ�(�t\\�\"]=G����,��{��>�\��!=(�o8%u\�	y��T\�E\�\��1\�Sj*:(�\�\�3���Z`q���?�QXn�,,\�\�\�Q\�\�\�\�Bf�!x(0\�k�i\�\�H��_��\�+�\"`��b\�f39�|73>�F����*^\�\�\�Z]]���h��\�\"3,���&\�\�2rSF\�2q/�s�Qn��(\�qEuu5U��~�a5�\�b�`er�Y0\��\�\\yk�)aƆ\Z2�w���-\�wPE\�\Z�\��>}:U\�fи�5t\�D]Y;��=F\\�9*F\�(�X���\����P\�iT�c!��Н�i\�:\�`\'\�@\n�!\����w$&222(//�����\�ߠ}Ǌh@\�n�\�w�\�M��E/��eI�hˌ\'E�ؾ���\�N\�d\�\�^�i\��1��W\�\��ZD&����\�Q\��O\�XT$\�ܽ��%�R�g	��G���\'O�vkxҼ�H\�\�\�\��@\�ݹ�6\�ΧA�ш���p\�\�D\�d\�\0�)hB r���zv\�f�$ʤ�$!rof:?X@\��\"�&L\�\�1��؟I�\�w\�����,���☺�\�D�A\rv62��@\�\�\03\�\��\�#�&rss�\�\�\�|�ejj�rE\��McJJ��֭[\�Z]N��Z\�\0\�\�)�[�\�+\�(���-a\�6�-q�7��*�X\�\�,�T�[�y,�,̉�;�u�j%�ϣ� =57\'OQj\��C�v\�>h1Oo\�2�3�	�ѕ\�\�{I>�p\�:\�}�I6ʪ\�F F31U\�\��c��C���x���H̼Nf˪\�<IL`�G��\�\�h�7�;=@5<H�H%n=M,B�~�\�Z�\�\�ƾ��\�\�ޝ�^�\�\�e�_2\�C�\�I�P	d �S<�����S�5���9�YV\�	W1�ͩ\�\ZM�x>\��\�^��r\r\�\��\�8�����\�Q�\���q(Q<�\�;\�\�5\Z`<sk��\�\�LŸX�£�\�!� .W�k\�T�2.&\�&o/\�\�\�RYu�S���W\�K�\�\���<|�\��_\�<�r#����\�,��J\�).#h�u⭐��1\��\��\�ϡx\�\�x\�.�\��r\�m\�Ɠ�\��(x\�\�\Z8�\�\�wؼAu�G��l%�l.\�g�J\�}<��/�\�]\nyH�q��ǴJ ~�����zP�8��M\��[Z�\�iF\�N>&�-*��=������2ȏQ�����B)l\"ĩ_=x@kǠ\��\�q0ޕ����|��\��2�Ø\�w �`�\�g��\���\���\\�7\�\�D�+\"(�1W�e\0\0\0\0IEND�B`�'),(6,'Розведення свійської птиці',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0�IDAThC\�yPWUǏ��(.��$.\�\�eaθ0M���\�db:&�k\�NYN��iiAff\�\�R.Y�2�\�(*B�\�\"������\�{\�;���x�����ߙ\�{\�=\��\�}���TuT9\�JL\�\�t���\r\�@2�A�>\"!�+�\�:\�*({�,\�X\�\�_p\�\�W�Y\"\�\�?�\n4��\�@	0\\S�#\�}\�G�\�8�7�$Lͅ\�\r�2��!3\��\�}	i�\ng1:.\�!T>\�[�&��\�\�$vs\�\���\�\�edH�*�A\�J�j!�I��\�\Zȼksv6W��x\�9s�\�}\�2^\�W.4�\0)Vm��e������,\�s\��Y\�]$���F�[u�e̘\�G�*ED���)i�\r&����\�\\�\�ca\�\�\�XI\�(�k�ӫ�\�\���1\�\�\�	I\�V0tƉѸ1���\�\�Nz��\�\�\�6��Z3>���jda\�(�jTvV&O�\��w\�YoN\�\�\\�\�\��o�\�}���n|h\�n�2<\�˂��\�#\�\Z\�\�ݫ;4ъ��\�\�݉�=\��\�8�\�b�\�\�\�;�x�\�\�塵�i\�\�\�2�2\�\�07kf��Sv�\�T�\��9U�Q�FDC�ʉQ.�n�L/��\�ô針\�ަ�X,\�\�̤�\�s)69�ƽ1��A��]\\0%\'H+�|\�	�����!Cd�\����\�_FȞ\�|� ��Ο\�\�x@�g�\Z\�ߟ���>\�6p�����#�sFz\Z\�Ŝ\�l�֭��gA\����BN��W��Q\�\�X�\'%�N\�!2��_8F\r1\��.К�\�\�\�P\�}��\�\�M�}B��nRv\�m:�w�n��\�S�иèw�~\�����A*wU(M��m\�\\m�u휵�����@�@rn\�L[��SA^>����&M�Fk7n��I\�\�ޮ=n��^�L>~�s\���H?�\�2]DkW��P˖R0��4A��&*(����k\�Ӄ�ЂQ�\�\�Ç.�M�kW\�i޲rtr�\�\�BJN<GW\�R���w\�-ZPII	=ۭ;M	�I\'��j6MH�T��4����\�ŋR@�蝈i�b\�\0Z4z�Vw�\�u\��n�\�\�#hF�C\�Nԣ�=|�r�n����f���Gwsr�є�_��J@�^��@���Z�mVWW\�\"\�yV��\���-M�\�\�K1|��5m���\��,\�W�d_7���0c\�x\��ެ\�`\�\�ܳ�3�5Y�i��r��:*��*���*��������\�\��Ҡ\�}\�!��R�4w��Ug(b�zZ��c���{�>g>y8{S\�\�zi\�pZ�|!a\�\�\�՛6n���{�`�0Hjd\�\�ǟAǑ�\�\'�Z��e\�D\�b�\n\r�k�;9ٜx6\�NMN\�@ϱ<�\�h޴b�f�u\�\'&�\�\�eھ\�\�\�\ZI\�\"TN)\�HGm6\�\�\�k��I�w�\�ș�Ԓد�1���03~P�\�M�\�N}�0\�eU��V\\\�=�\Z\�,\�\�(��;9�\�\�\�*\�\�r�m�9S�\�R;v0�kg\�[Gm�H:F�\�\ZT�q���*�)59BC�\Z9E��R�\�h[�N\�,^̜�\"T �c��6�a�\Z�\�$�2�y\�h1�Y5\�\�O4x��]\�\n�<޺Et\�\���X�\�\"U-�C�P�~Z��\�>��G\�	_�\���r�\�\�ҭ�A\�\�\�A}󪄭�Щ�o夾�MB\�\\\�RΙ�Q����\�B\�f@�\�h\�\�K�\�N\0{��j\�/�Q\�\�Mp�c{�~����5F[�\�n�+n\�\'8U�~�\'A\�BŻ��\n·�\"�`5%������\nX��Z\�\�_��U\�\�&J���5\�?v\ZQ�b�t}tBP��g��5a�E�=Z!p�$b���Ǔ\\���Z�$!kN�\�\��\n��wd�$�H�\��d	5�%\�[��<!9�\���zBD�?JA\�l\"\\�\0\0\0\0IEND�B`�'),(7,'Водопостачання, каналізація',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0�IDAThC\�lTU\�OP\�B��]\�J:ݤXAm ��HX\����\�\r�����l̦&D�\�D\"V����1��l���\�\�)Z��NW\�\�\�Rg\�\�\��\��\�w\�y�\�M[���\�\'�\�\��\�眹\��\�{C?s�\�Hy\�0Q:�<(����!T��\�\���f@/Ag� \�\�����9b\"z�\���\nAN��\n\�a17v�\�\�\�(\099�V�\�%q\��Ѓ\�e1<��C�\�\��OA�b0\ZR�ʟ\�ݵ��@�b(�RA���\�Cmb\�fHy���`\�10\rŗЯ��\�H\�\�ٳqo\�O4ovAl�}}D\r\rD����=*�G�	��\� ��ɯ��\�T\�\�c��c3[�l\��\�\�KK������SR�mY���\�,�\�M\�;���V�455�֭[w*\�\�\�qQQ���H�\�\�ʼl��Ͱԩ�/\�\�A\�v\�`�֬a\�\��\�n��7o\�\�YYY\�Vӭڰa���\�h\n1?���\��a�tL�\"ߵ+W2���\'�+Vp||��\�\�˓т\n��\�\�GX.	+|�\�\�c(��!\�\��<I��->�׷���@-]4!)���\�\�\�\����D\�\Z�)�L��o�G��\�#3�^\�.\�Ф\�G6!�bU�\�M�?Hig\�&[p�\�^:u��\\�M��� �OJ�Pl��O\�ͦ��\���)�T\�\�@���\�$$\�\�!\rG~/�%�)�L�L�q�4\�|S\�Dɉ�9i]�\�J�\�_H�/���;�T�\��,r{.K��\�B<�#>�pF�\�\������na\�\Z�\�di�9S\�H�r��ů�LIƪ1114-7�\�\�;� o2yꚥǄ��\�3Ұ�\nae\�+�~�=\nŒ%R	�NYC��\�pe\�\�\�N\�\�V�`�\r0-7�\�!05��=@WW�\�ĂR�eU�̃�\�I��lƊ��z.\�����C\�\�\��P��\�\�\�u\�@�P\���7�t\�BGsR��_y�\r� \��o<ΆP�C͞~��\�\�y肷�*k*\�\�\�*��\�뺒�+\�*��O��[HI!JL��\r}g\�\�*\�\Z�\�҆�G��\�L��\�ԅZ:\'�\Z[q��O�\�-&W������\�\�\����h\�gg���0\�\�b}\�.M��\�\��\�҆5~<s[��\\W�֡S��\�ϲk��;���=��^ʋ6.\��\�G\�r��˺>�\�\�}\�7\� 8p\�4 �ӧŚ���W�^v�us\�o3x����\�\���\�s\�X~��\�\�\�m\�\��nz\�)�^�?C\�\�)V��fU*]�eV�\��C;��\�t\��P\�\�\��\�.�H\�\�\�\�;_�\��85���/D	�rZKK�\�N�����LJ+�|\�\�t�R\'&b;	R�;@��*ib\�D�5���Y��\�;}��i\�]s�����c\�Q\�\� H\�\�q)\�`Y\'\rYfC11��D���\�\�\�\�\�\�\�\�\�~�\�\�<c\�>�\�\�o�\�l�-H\�C%<s\�L�\�z�濸~?�K��:\��\�\0\��6\�\05(�-*[./�W��\�\�3))�)sQ&u�t\�\�\��,L\�u3\'��`�<4\�5���\���q!Mx\Z\�J\�+�>DP�J\�\n|Q~�]ȊCHW�(��^����\�\�\�Qo��S�R\�\�\�\�{�}_\"쏮Щ2O\�j\�?~\\;Ql۶\r��+++初\'N\�1۷o�#��t��\�YΪ\r��i�]ｧ��4���[��aPc|\�DQR�\�l\�\�%�\�`���\�49�u\�/�\�Ѡ\�G\�˔�\�\�������S&Eָq̫W39\�H$&\�\�\�1\�k�\�ҜmX���`��|c�B_A\�_�4+?\�HnU]��H�\\�#R\r݋��Fs \�\�M�.ڊ��F��B}&#\�R91�L,1��\�knĴzD09�\�G�W\�\��#\�3���\�h\�me4�\�~�\�P7����q`L\�\�;9��)�\�uA\��\�0Z}\r�\��C*\�pr<\Z�\�\�\�b.:��[&�\�\"�%�3\����V\�\��\�gjT�\Z9\�}���\'��a:T&�O ��\�\��\�WY\��iC(W��4 \0���$ �\Z�\�2\��dBU��z~/�\�[�\r��j\�?�\�\�[�7f���\�-\����**�Gl\0\0\0\0IEND�B`�'),(8,'Виробництво гофрованого паперу та картону, паперової та картонної тари',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0�IDAThC\�}LUe\��H�$DJ$f*&\�4Ltb��\Z:▛\�\�\�\�2�\�̅�YY\�d�Y�e-�ӥ\�[e�\�jN\�4m�/���\����<}�s~p\�\�\\�.\�~�\�9\�<\�\�����\�\�\�!�ĉ�=B�\�G��\�#P\�sa&:\�v���\��p7�	E7f�T8�C�\rH0.�ٰ\Zӕr\�i8�C:�/@�\��*=}8|\�@�a4J\��\�Nc?<\0&\�6h��\\\'\'!\\]�\�Y�.\���X�\���\�Gи��3Qh(\��\�D\�јǘȞ�D��D���>7�22�R�Y�F\"\�\�\r���\Z�j!\"#�HO��Zt�\�n��\�R��\�8}נc �\�\���b\�6!Z[9�\�\�\n���)�,W.\�tp�r)\���\��\�\�C���\�\�\���K1^�Ɠ\"%��X�\�~��}�\n\�Y����\"e\�\0.\��p�縣�K�rts���i���\�\�%�,��iBc]-�L����>�j\n�dNL�\�*�����ф\��mX#r.�U�O\�\�)6D�)^��r�8/K\�~#N�]����\\��ˁ\n֊N\�yk\��\�DÇ�\�_��\�g\���PQ\�\rj��\"�aO)\�\�|�-f]�_�\�\�\�.c��r܌���\�\�U�\\ *�\�\�e[ǒ%\�\�(�~�FO�LQ�%RuY1�\�\�ҼW\�Ҫ�~P��*)��_~L֓���=���.g�h\�\�t�g�ż\�|�@�\�9�\�blBUi�47�\�\��\�K�(h\�4\n�\\@\�.\�\n�\�\'��u�J�\�&�\��\�\�\�B\�Sߣ��\rJ�N����c*o;�.jӄ��A�x���\�U\�-�Q&+�.�.�\�-ԣ}\�̉K���\�(\�=o�N\�f\�%�gB�wug\�2\�x\'��\�\�\�OUy\��\�9^^\��\�B}\�\�+�\�(N+ȉ<{�\�*N�\n7s�R�\��\����\rM�M具\�X\���	H/:T\�\�\�r��@\�\\~\����@�X;�3\�y�P}U\�\�ܤ:>VY|G�HW��\'\�`r���O*\�h�\0u�G֣ܝwhLi��i����\�F\�\�\�<��r/��ƺ\Z\�\�LW2N(�5�J��������L�!\�\'h<\r�,ݽCX�(t�|\Z?+J�\�\n\�/\�*xO�ÎuH\�\�G�X:V�\\)\�\�\�xϘ\�\�Z+����Dxx����\�Lj�>�������)v\�$)�m3\\0on\� \n\n\�*999���D���4e\�>�\"�Ϧ�&ھ};EDDP^^���SYY%&&R|�\�r�hё#�cF6Fq(\r\�,��W��\�/Wc�\�͸\�\�\�V������\�\�\�\'\�\�\�\�̙3Eee%�\�\'d��\�R�)�E\'\�a\��\�\�Q\�G%mkk\�#\�֭�\\�\�\\��m\�Is\��\� \n���5Υ:�\�\��� �N�..Bdfr�^�\�#/6Σ�7��\'oYt֔\�KVg\�		\��5\�p9zp��\�\�\�\"O�\�Lv\"\'H\\�q\\\�\n�\���#a�O\rV�V?*[hlT_Ԙ1Ʊ\�\�ťX��\�-���W�\� nNe���q��6	1v�\Z\�0�N\�2��N�&�l\�\�K��@UOO\�>]��K\�\Zt��M�4\�\�Ka\\B���\'O�z\�g�\�v0(\�\"H_)\"rZ\���`hۃ�\��.���\�\"��͇�r:���a1s��әǩz�D���0\�Oz�\��\�[\�,\��#@@G���Y�\�_\�=A}�������C:�{S\�w�\�\��`�IB{�\�#���v�$�\�J\��\�\�\ZH&�\��ɻS�2\�?�T�T=\�Et\�*�=]\�O�\��\�B���D|0\�u�<`ԡ�\���s�\n�gaG�\�o�{AA�{?A��\���\��	�7�?�\�?�]c\�\r��\0\0\0\0IEND�B`�'),(9,'Виробництво інших основних органічних речовин',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0dIDAThC\�L�W�\�?0,��\nLK�\�\�cA�\�*�S+1��\�G�ƬT\�\�hĴ�kJj\�ݬJM�\�\�MVkk���\�\�*jaEW\�*E����jt�-��\����\��\�\�@k��%\��\�=�\�s_����E��D�\�\�G\�\�O\�૯B\�\�-\0�C}	��n�?<&S��(\�\�0BN\�;\�\�cRүq\�\�3؅\�	z�\�?h��\r33�c[�E�\ZZ\�1#��V*&&�bP\�\r�#���6ajj޾!L�\�l��[���\��J��*�p̘���?�\Z>\�\�PK-4<\�d�;M%���\�\�~CN��\�|��\�qܸߑ1g�J��^�3\��\�d��V���q�\�0x�\�b\�\�x�\�`5WB@\0��I\0II\0��\�\�eܼ	p�\"�g�|���\�^X�r+��m����\���Ƅii�\�\�hZ\���\�O�B\�\�\"p�$bA�܎S�ׯ\�.>�\�/i��I\r1IH@��KQ�46\"�/�\�\�d�?�|�pC\�\�D\�RpVj�ɌHG\�8l6\��t�m�BDEU\�[&\���H� /�.]�O�+\�\�3\�\0|��ȑ\�\�\�	555\�\�\�\�\�!!!\\w\�Nb�ۡ���F#���É\'�{&�	\�ǎX���\��ea؅]]��\�766��<7�wo�\�\�%-C5\n���wc\�ƍ�\r\\fP�VWW���y�pǎ<�h\�\",++s��}}}�֓\�\��z�<�\�\�\r�\��\�{S���B\�\���\0��<y�Z�e\��f\�\�\�¹s\�榹��p\�\�5سg���\�\�\�a�mmO\�믏���g\�̤�n\�\�(z�\r�`۶m`�ل0y�d�p\�\�ܴ��@rr�\�TV�^-RD^�j[\�h8~|�\�T��\��\�\�\�;�\�hy\�%w\�XUUO;���\�1�����\�gD�{\�e�H!�*�PT��������aeJ \��\�Y{���\�\�P�v\�z�J��hX�V����\�r�p\�S(�4�9���MMM\\gtww�1}�Hh\�\�y��iFA\�I0:\�JS����Þ�\�1�\�O���G�\n\�7ؐgdd�`�E;�n郩S�44����\�J�k&9e\�D��\�;�v\��G\�\�H\�f3VVVR�\�iӦ�\n+X,�\�T\�{�Rɤ�D�\�w�\',\�\�\�\�\��\�\�@K\�ck`ii)�����+W`ժU<_\n�E��d�����\�tVH��k\�\��\�\�y>sn\�ܹ�n\�:j���\�\��:h\"\�\�,\rd��:\��\�`���V(z\naʔ)���ɝ۹s\'lڴ�;���K��:�w\�%4�Sϝ;B\�@��:�t\�=�_U\����X�d	lݺ\�\�롡��yzz:�Y��\�\��TU2��\�\��p��qn���>�N5��\�/��,�$YYYT�\�:�\�����H�\�\'��=�\�&	m��@/\�?\�5�ʸ�t�\�W�\�\n\�\r�$\�ϻ�\Z�ĬX����F!\�\�@-\�\�\�t.-S�|��ܾ�\�DG\�U\�\���\��I�~��\�\�O���\�\�y�-�G�\�i_a!1s&\�Q�7\�\0()Q\��AZ\�\�(g\��A\�*�\�\�i�:�]΄���\�L\\C|\�]��\��\�ŏ?�9�\�q� ;\�DG����EV\�f����N\"l]��Ń��ޫ\�>��\0\\�.r5X�\')�2hy�+r\�\��\�\�Z\�)l\�\�q\�I]\�O\�m�\��\�l\��۷Ǡ\�xSR\�-���曈���`\�>�~Hu�\�\�N��\�I!SLIa7�{�\�Z7�\��\�\�U\'d47#n؀��\�r�ŁO=�[*��nx�%%���]Sx`�\�<Ο�����t)\�\�و\'\�\��&F\�%\\�\�}���D7�\�R#\�\'LMe�A����\�ϠX\�O�%4�Ũ�\�\�B-	D��ORcC/=8aB	\�y���\'����\���\�\�Њ\�|�6t:V\�\�[MF�⛠\\:�\���\�|;:F\�#��\Z�_WAC\��\�\�����3ڣ�-��?		i\�\�\�	���Âg\�bCݫ�d��(��V.�ybx�\r]M�*�\�waC[�\�\�^nK��@\�rBR�o\Zz\Z\�߀��t3i\�\r�g���*��\�,an衡��gU֭���b�<m\��anx\��f\��ӄ/\�c9WFe���\�o\��\�\�UTq�\��806v���\��7,����\�\��\�ؿcEE�(�Âv���$G\�\�m��\�͛��\�\�\��\�\�Nr\��~�����0\�Gu\�	�G\�\�ѕ\�\Z��Ĝ��\�9\'\�\�Ag\��o���\�\�\�r*���J\��G�/�\n�\'ɝۊ\0\0\0\0IEND�B`�'),(10,'Постачання інших готових страв',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0�IDAThC\�{lU\�oˣM\�\�\"ȶ�\� ��\�\�\05\"�h�\��\�PB$!\Z\�#A@���J,�\0FE	$�\r�Py� ZXJ����.��G\�\��Nwf\�\�>����\���sgw���<w*n\�J\�t\�PAa_w�\neE���=��\��*�G\�!�3�a/��QX����\�`� v\'+S!i���\�l|�\�+�޲2u~���Е*LLRu7�����c]+9+�н*�&�@���b1,SV\�<\Za%\�\��$]�F n*�E��-��\��sThƳ�\��2d��}���Æ�\�\'\���t�\���\�Fm�h\�w@\\ٴI\�q��\�%\�R��@��b-\�\�r(�)�\�\�\�Zo\Z �~ɫ�6��!r�\n��\��De\\NCa٥��&5.Ξ#\�z��r D�V�\�5!\�#.q$g�B\�\�H�eo��q\�#�0�ѵcQ<�\"\'�Y3\�\�\\RYuN<tJGf(2s�h�\rô/qt1.\�F��\�\�\\\�\�\r:r7Jak\��$v~W%\�\�%����g� cg�\r�\Z�ߣ�[\Z�E \���\�^Vh8\�\�8N;\�\�Z�r�h��Ow &??D\�DiܶM�3\�E9�\n\��\�\�_C�@EQ\�w����𹺋bZkZ��\�_]���7\�ٵqZq>.�c�\�pEa\�e��	\�m\���l=!-8ؓ�����!�\�E�\�\�E�|��c8��\�H���^�\�\��\�\�\�N�cG�Z\����\�lܱg�i�~qa\�$957�\��\�\�\�z�QA@�QLqSCX{�a��}���kz�\�F\�Zf8A\\YJ��\�\Z.��ߝ��\�ܳE��\�i���Jǚ�=`�%ǋ�ܻE��3�E>\\s�ei�\�\�2n��ȂK\'\�\�#�ē/\�q��@f�.\"{\�=��7��1CƎ��\����\�/\��\�\�\�\�DUY�d˱\�\�r�\"\�\���\�ő�Ӭ�(\�HL�\��<��z�8�Wu\�<�\�䒡-�Ԃ�/\Z\�Mʉ\���u�\�k����\�?�#\'\�\Z�\�\�6��K�L�\�\�\�\��*�V|�k�D�\�\�\�X�b{��j)0Xs\���\�\�\��;\�e�r,����c\�g8��Z�|�1\�ʷL����\�\\�c� �F��%��(FN*���2\�7e\�Z�ï\�\�7�߹�\�\�\��^�\�P�5��}\'���\\EN:\�,\�\�ؖ��dv3ǡ�3���I\�}\�\�\�a\��N\"O~v�=Qp71�ںm\�\�J�¤�ɫ!Xr\�E�u(~S��-)e��?\�-q\�>�\��3�\�𑞳:.�^�\�l\�q�F�\n\�\��H*\�Ā\�u�l��CA\�<bq�7X�\����x߽ą{hv\��5~�\�SM0[i\�R�\�ѓpr�B\�\�*T���\�0׵xpQ��;�\n�\��@N\��\" r�GUt˘q\�\�];I,^����6\�\�x*3\nē�=�\�o̤�\�7��ԫ\�4n[\�\��\\�n<≘��SQ���`~\�\n\�\�kA2\�\��K!\�3\�\"q\�\�\��m1a�V)כD-H�L9\�\�=�\��\0,���V�7	܈/3�o\�N8kW\�\\k��@\r\�[�_)�\�N,\rK����≙����f<�:=��:\�M�)���wT�2|\�\�w\�\'U�)%ث��`k&\�5\���H�[������\n\�!�Ə8\�K`�tu}$\"��\�>\�Y������\�\�\�\�\�(xLp�W\�T\�A�\�;\��?��H 2ӥؓ9f}\�`\�)^�1hc�� �}O=�\�Zq�\�-hQ�_\�=-M�M�\�&@x2\��փ.\�\�\��\�?\�cU�$\�.\0\0\0\0IEND�B`�'),(11,'Допоміжне обслуговування авіаційного транспорту',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0	/IDAThC͙{pUGǿ��{s�ܼ	$\�UHxCã@�\"C�BK��X�\�\�3822[p��Z�j�Em;P�:VQ^Q+Pka(--y��\nyp�Inr\�\�]{Φ����~f�9�{v�\�\�\�\�\�9W�Q��)tYB�i��\Z.\�a҇�:\�\�/\�)i1\�/$��B�$=,L\�9\�\�B\�aR��\��Hu��H�=d$��\"���\�\�\�(BzSt5r�q>i)JR\�\�N\�)Kt;<�A1\�0p7\�K\�\�Ǒ���\"�ov��\�C��\�\'�g���Ţ�ww�/G�\0N�RvkZHs\�b��U��Ӓ~K\ZzX33۰���z\�<6$��1V[�n\'Y\'�+\�P�o��\�Uk̘^\�\�\�Ō���\��D\"\��\�l\�\�\��$�\'��R���;�Ѝ�֘��Z۵��hT�Ș\�/��M\�\�kk�u�\��\�G�]����&��nv�\����6�\��\'�f@>\�dt\�?��z\�sm\�੧����u׳�\��_\�`�ŀ/OF{P�F[����ۮ`Q\�r�o�l�yp\�R���4�G�\�xF\���ǅty��Uy�e\�\�\'Ef�\�h\0[��\�D\�wv\�C�V:\�Y9�\�#߀5\�\��\nD��m@m�Ȩ��4S�eBR{܌�Z\�X_��x\"����\�\�r�5ݼ̶\�|���\�k\�\�玲��G�\�k���Fw7c��\�}*�3�MC���\�<y÷Z�C��8�\�x�\�nt�;�N�\��%\'|��GQ^4�F-XN1jW|�&P8Jd\�^`\�\Z�I��T\�|��\�\�g�^��!\��\��\��\�\�\�F\�%d\\\�Ɯ)+.�3�\�As�\�h��i�VlX�\�E��x<J\�\�I�\�sp:)\�9\���\"1�\�\�\�\�]/�q\��6���ASZ��\�X;u>�]%;S\�1\�ju0\�S����O[`\�H8m��\�։�*s���\\�LN��C$`J�BWU\��2,YXs��&�\�4_�39��0�!�~Б\�H���Gߖ\�\�z�,\��S\�	�++yo��\��y`2Y�I�\r\�hn��׍�\�vd�\�P9+�=B.z\���Nt&���\�D�\"�D9wШ�(*�x��\�\�Ư�_{�����?/\�g\�\�S3!\��c\��b�\���hM�]Z�<S)�>�9\�,��\�>aI`�((�$���\��\�d�d*˫\��z�Z#\�/�}\0sN*�4�G\�	�\�~	,d�15�\�_���y�(\\�|\�Z̨��^\n\�Qz�2�\�e�@�wP�~�\�\�ހ\�\"N4u\�`J\��	ࢽ�\�\Z�\�j���v�#�f;��\��i\�h�WP��\�l9-\�w�pXI��k)�9�]$\�\�u\�\�ՙ���`\�\�\�W�^��Ì�\�E\'E�{��A���h�\�\��$$\�_I\'p����Y٤\�r@^0�a\�1N�%3*\�\�\�f�\��j\��GA_�O?zzD&�&\�\�%%��\�46�L2�\n\�)\�11�TΧҪβ梘v\�Ŝ�\�\�\n\�\�Ϊ\\ \�Q\��i�P\�wp�\Z���D2|H9�M#\rI�\0\r�[\�;��������=\"�\��\�US��d�	K~��)��axBZ<m�\�l\�G\r\�\��\�,\\S�؄\�\�<(`9�~ۜ�p�&X\�ڌq՟\�����\nk9fdW \�(V\�ٳ��Io���\�y�;\�\�_ͤ	rQ����W\�\�\���h�:᧕\�\�\�Қ�;\rחzQxH��i\��h�8Q@��d7\�\�\n?&\�i\��\�ܠ܎S�^�E�s�\�k�(|�\�4�R��/rJ�Â*\�~�G\�\�G��\�3\�8�\�\ny\nGi\�\�\�\��\�)\���xh�q� E\�\�\rw+N�#\�%�$\�B\"�\�TP\r|&)�[�����F\�\Z\'2h�\Z�\\��{��\�=\Z-o����̙�\�t\�\�i��yW˒\"x}�\rVoD���W�����A�ѥ�4H,��dVn.APe߱\�k�!��t8	Z)hߔ\�\�9\'\n:�\�Q\�Ď7l��N;\�\��#- +�b� \'��N8�\�S_b\�W7NBG&��#g�U/�\�l\�䎄W�d6�c/�D��+\�RG\�vU\�f-�-\�\�\�\�hE\��Ob��06\�\�Q{҉4�\�[(�\�T5�\�#\Z\� ?\��]&\��\�\�X�NM:>.OE�\�b?z���6?f5��G}X\� ���c��D�6�t<\�\�H�/-#�ˬeV�,�A�Zg\�#M\�$C7�\�6��|/�\�\�\�P��Ic�\�MM-�\\\�\�\�P%����\�~ \\�5Ty	)\�x�u�4��Z\rjPc`���\�v�P#�	�+\�\�X\�U\�\�ȡ\�\�Hc�`n�}��%Ԙ/�?cwZn\�j\�\�\�!#|���9zm]\�>d��t\'�����\�6 ��H����\�h��+�\Zd�@LG�\��\�ܝ��[H\'b:�^\'�\�09\\��h>�D\��\�}졎\�#����#~B*\�\�\�\�\\RH80��\�p\��\��cR@8�&\'\�1Q�\�@|�\�Jt�����ڽ��\�/\�\�/\�\��ȡ$�p!\�\�	\�T5\���w\����.A��\0\0\0\0IEND�B`�'),(12,'Перероблення молока, виробництво масла та сиру',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0QIDAThC\�ipNWǟ!�\�d�\"!�E�b�I�!-e�3\r:\�L�Ԗ2��)�McSU>tP��3��\nU��$*4!��$d�E\"��s\�y�.��ٓ�\���\�s\�9\��ܳ\�s_z�\�D�\�#z\�H\�ɗ�\��x\���[����\�>`\��\Z�ۀ�0\�7���V�P{��c`�0\�{�\�\�\��渫�@\�\�\�t��&w��pcTF	\ZE>����DMT����*+�rr\�0\�W>Gyr���Y+p�knn�m\��XNcuuL��\�Μa,*�1u2\�\��\Z|ԃ��bl�n\�*+E$�P~>c��J[Zr�뎅\�+@�Nc-���%%	�\�о}J�ZN���@�N#-#G2VP <�@�/+m\��=s�E]��	(�\���\�{���b���r!6V߾�|�� q\�T���l\�.��%N�t��?A\n4#=�1OO���\"$}\�\�J��2���=.\�u]�\�gA�lJ`F7��Og�\�KMU���!B�Զ\�\�E$\�%\�a\�߹S\�\�\�\�Pii);r�0%___\Z\�\�A���dBw23i\�P:��[�~QM_m\�F+��SEEYa�|w\��:u\nE\�Đ��%6\�.ee�m�h�\����Cr\0\�e=\�\�K\�\�F߻G��YCE�Et��i���K)\�)���Lw\�fӉ\�\��	�N��];vPXx8]O�F\�o���)�jj�ѣ\�izH��`�kxb�+W�#=աK.�fBzk9�#++��\�\�?���\0\rwr�\�\Zy��\�ǋ\�\�_/Т\�K(�\�o4A\\8w�~OL�\�\�V\n�7�V�^M���\�5I~~\"�P�H\�\�i��1c�w@`\0U\��d\�R�����Y\�r��\�HBݺy�v}GWRR(3##WFuuuR=.��\0�>#���y_�\'2\n�bT�\�	��h?\�rrq�M[�\��%�鋍�k\�Sԧ\�\�8lM�4�\�\�\�)\��}jni�8\�\�Y���z�A&�7�V�QZ�\�\�r�0�僄&@��Bp�Qss3l\r$\�#\�	z\'�\��\�|*))�\�c.\�X0��\Z\�jk\�h\���R���7c�\�SEO��\�CG|-Hä	=����!z�U9d����\Z\�\�?0PZ�>�,�#�(r\�B\Z�9\�\�\�A\�\�\�\�=*�\�ӧ\"��&�O\ZgD�%�}<���xns.\���\�\�\�M�X�/_��\�\�F233\�-k*.�{qnX6�V���\�<��\�W��ᵳ�Q/סCDk׊����\�\r�\�jz��˔B0�\�\�\��:��\�\�Z\�+�\�͇��\�0rqv��~��,j�\�i��y��ڔ�=!GG{�\�.]��\�xF`�H�:%2�<\�J=\�\��m,��\�!\�i3,(�ri�êQ>��^���-�\�\�_9k ��iidaaA�\�ϚЫvm���\�3\�⊊N�\�[J� \\�Eb��)6VZm--����A\n�=\�,m5-\�+_#w��\�U��T+D\n_\�Z��q,`�V�f\�8(��=\�\��r�E\�_����\���\�C\�\�n��\�\r�}�\�H&>�k\�u\�(/\�+,,%�g\��3Gݶ��\�\�a�\�J�a��Fx\�23��U�����g�p�\�M\��\�Όᜇ�\�\�QVc\�\�1fn�nK|�\�Ko)�/?\�\�fC��\r\'�6\r\�\ZoiC\��\�x��8)�(+�w\'��yu!\�h�\'\�O�i\�Z��Q�B�бPy�A\�\�P��	�M�]Ѹ�\�2F�\�\�h.\�\�^\�ho��k����:���\�`�p\�u��=\�?8�\�\r�W\����Ԍ���\0�\�\��`�����uU06T	\��.a���!�\��tǤ\�A�w\�1:��C	p\��Fp�î���D�\�;�	\�\�N;�\Z�Ӷg� [8\�\���i�	N\�A��=���m\�o�������vQt$\�\r���t\�;AT��@��n�O�,q�\�\�\�]�F�㧟Q|�7@D�c.ђ�_�e\0\0\0\0IEND�B`�'),(13,'Виробництво пива',_binary '�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\0(\0\0\07\0\0\0~~#\0\0\0sRGB\0�\�\�\0\0\0gAMA\0\0���a\0\0\0	pHYs\0\0\�\0\0\�\�o�d\0\0�IDAThC\�{PTe\�_\�+CRj��r2��L��\��S^��\�J\�H\�F\�\�XQ48��P�\�%\�ұTȩ��P\�J\��d3\�B4[TDVО眳\�\�˞]��3\��},˳\�w=\�\�m\ZI3-My���ݠ���\�PI��eW\�jpl�\�B���\�\�PkȈ:\��	\���\��\r\�X;�\�\�Po�97��P.�nTZl`\� \�\r@X%B�\Z3b34F��Us�\Z��\�^�����cs<LҬ)�ـ��h��͑P��c�Z5\�4��\���������z)j3\"Y�.\�N���C����\�ru\�V�Ք\�P&2�\\�zch\�\��B-�Z%&Jx�\�T��՜\�+�\�\�\\�fM&�\�j��A�K@�\�4x@34�N��q�\�E�|v�\\CfM8\r��\�\n��b4B�\�\"\�[�9¿�Z�\�k(��P�\rxD�\���5o\�\�Ӥ\��\�Z-8h�-_+CĄ\����֍o�;\�&��ggi5�\�!۷oW\�-5��3[-��\� �B\�y�v}\�\�j����\�ʷSdHO��\�z);[+\��>�ɒ��*���\�\��a1q�Pw�\�S�x�Ʉ|3*mSR\�\�HΜLY�.�#e�\�=�2 �Rv�˗^\�b$??_{�5|_�,��ƨEoCk\��q�8\��-�a!\�@���ܐ�פ\�Q!s���ս%��@��S2m\�Ѥ	��\ZD��_��\�	\�\�w\�9�\�\0�\��\"{;epB�\�L���\�\��\��\Z9�O;I�rʨa�f�\���i%]�\�\� ���Zl���B����$qZK\�n5����\�o��=C\�\�Y�\�6��Mo\�\��meժU\�Xr���N,��6M\�\�ʒYk둱V2aX�Ҷy�C\�W��\�eʘygr�<\�5T��\� �,Ζ�4�^zLƠ�;�C7)\�P�\��2.\�VSg3��|f�b�.wJ��2�O�;Z\"EEEJ{\0(I\�`||�d/,�\����:39}t�R&�@K>��\��P)�� c�J\�\�le�K�<�����$kJ���?��\��P)�O�+wF;d��;1VM�\�M\Z�z,8\�.�j*\�&oF߾}����wj�2q\\p�\\;��d<�,I���\�G/�\�C|\�r��Ω\�h�f\��f���\�Q\�\�/e\�,\���Q�\�wZI\�1\�$e,(Q�F\����\�c�d����I_�A�v>�L \�l��\�*�}mTx�u\�ڥ\�t��E��8a�Z�{ \'N\�\�}��i-\"C���c\�*e�̅)�V�!i�\�ip:�^y\Z��6\'GOT�{���V�y\�O|�\�\�\n\���%Z͐.s\�w�)Ԣ�z,	\�F>a9���\�]D\�,ރ\�&�բ���%_d�\'\��d!h\�1s�ҭ~>\�do�VV0zh*G\���%\�\�C\�=mu��ZL��XJ�T\�\�<�\"z=�|\�3�f\Z\�s#�p8P6Ly\�\�\"�\�12\�\�+�C�g��\�D�[��\���Aע�\�ۨ���!>�\�\�$0�\�Z�\�p\�-\��4�5\�B�3�	\�s�\�nS��#ފZ^�5 \�\�ՅU	\�\rw�&\�}$\�7�l��Am\\�݌��⭭%�e\�,=\�\�Qj�I\�\��cH�\�\�]\�b2�;\�6�<;戭d�Q�o �Gek�a\��\��\�x܆0\rj\�\�KJ���l$0ɍ\��3�1|����A��\�.�\�]��.��\�\�\�ܻj\�>eЃ��=j\�/4�A0\�HP�Ϫx�gg�^�볠��]��{ p�0�|\�)\�| ���Q	L�X^V�[OO�\�=\�Lo-\�\�t\���<r�\��p�2G\Z�AZw\n�^�\�����&#�\�Z�6�\0\"�_�L�f�\0\0\0\0IEND�B`�');
/*!40000 ALTER TABLE `type_of_object` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user` (
  `user_name` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `id_of_expert` int(11) DEFAULT NULL,
  `id_of_user` int(11) NOT NULL,
  PRIMARY KEY (`id_of_user`),
  KEY `fk_user_expert_id_idx` (`id_of_expert`),
  CONSTRAINT `fk_user_expert_id` FOREIGN KEY (`id_of_expert`) REFERENCES `expert` (`id_of_expert`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('3','3',3,1),('0','0',0,2),('1','1',1,3),('2','2',2,4),('5','5',5,5),('4','4',4,6);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-11-25 11:15:01
