-- MySQL dump 10.13  Distrib 8.4.5, for Linux (x86_64)
--
-- Host: localhost    Database: ruttero
-- ------------------------------------------------------
-- Server version	8.4.5

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `drivers`
--

DROP TABLE IF EXISTS `drivers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drivers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `surname` varchar(100) NOT NULL,
  `national_id` varchar(20) NOT NULL,
  `is_active` tinyint(1) DEFAULT '1',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `national_id` (`national_id`),
  KEY `fk_drivers_created_by` (`created_by`),
  CONSTRAINT `fk_drivers_created_by` FOREIGN KEY (`created_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drivers`
--

LOCK TABLES `drivers` WRITE;
/*!40000 ALTER TABLE `drivers` DISABLE KEYS */;
INSERT INTO `drivers` VALUES (10,'string','string','124124',1,'2025-07-10 21:38:09',5),(11,'string','string','string',1,'2025-07-11 00:14:49',6),(12,'mi','driver','2222222',1,'2025-07-11 14:53:11',6),(13,'carlos','lolo','121212',1,'2025-07-12 14:45:59',5);
/*!40000 ALTER TABLE `drivers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fares`
--

DROP TABLE IF EXISTS `fares`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fares` (
  `id` int NOT NULL AUTO_INCREMENT,
  `description` varchar(255) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `is_active` tinyint(1) DEFAULT '1',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_fares_users_createdby` (`created_by`),
  CONSTRAINT `fk_fares_users_createdby` FOREIGN KEY (`created_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fares`
--

LOCK TABLES `fares` WRITE;
/*!40000 ALTER TABLE `fares` DISABLE KEYS */;
INSERT INTO `fares` VALUES (3,'string',11110.00,1,'2025-07-10 21:25:58',5),(4,'string',12312312.00,0,'2025-07-10 21:38:18',5),(5,'string',1232323.00,1,'2025-07-11 00:14:59',6),(6,'f',22220.00,1,'2025-07-11 14:53:42',6),(7,'fare12',1232322.00,1,'2025-07-12 17:00:26',5);
/*!40000 ALTER TABLE `fares` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trips`
--

DROP TABLE IF EXISTS `trips`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `trips` (
  `id` int NOT NULL AUTO_INCREMENT,
  `created_by` int NOT NULL,
  `driver_id` int NOT NULL,
  `vehicle_id` int NOT NULL,
  `fare_id` int NOT NULL,
  `origin` varchar(255) NOT NULL,
  `destination` varchar(255) NOT NULL,
  `date` datetime NOT NULL,
  `status` enum('pending','active','completed','cancelled') DEFAULT 'pending',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `driver_id` (`driver_id`),
  KEY `vehicle_id` (`vehicle_id`),
  KEY `fare_id` (`fare_id`),
  KEY `fk_trips_users_createdby` (`created_by`),
  CONSTRAINT `fk_trips_users_createdby` FOREIGN KEY (`created_by`) REFERENCES `users` (`id`),
  CONSTRAINT `trips_ibfk_2` FOREIGN KEY (`driver_id`) REFERENCES `drivers` (`id`),
  CONSTRAINT `trips_ibfk_3` FOREIGN KEY (`vehicle_id`) REFERENCES `vehicles` (`id`),
  CONSTRAINT `trips_ibfk_4` FOREIGN KEY (`fare_id`) REFERENCES `fares` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trips`
--

LOCK TABLES `trips` WRITE;
/*!40000 ALTER TABLE `trips` DISABLE KEYS */;
INSERT INTO `trips` VALUES (1,5,12,2,6,'bsas','cba','2025-07-11 00:00:00','pending','2025-07-11 15:37:31',1);
/*!40000 ALTER TABLE `trips` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `role` enum('admin','client') DEFAULT 'client',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (5,'string','string','$2a$11$mC32rX6ep4f/O0r3t.JA.e6DVB3gGWqVa6wGSkLzAw.byRlhSrXoq','client','2025-07-10 21:25:24'),(6,'st','st','$2a$11$X0qIt72ekDwbEFyAUX0NJudH2ZyvKavK6wxdTRUEECGvhKIH1cU2.','client','2025-07-11 00:13:15');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicles`
--

DROP TABLE IF EXISTS `vehicles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehicles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `license_plate` varchar(10) NOT NULL,
  `model` varchar(100) DEFAULT NULL,
  `brand` varchar(100) DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT '1',
  `created_by` int NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `license_plate` (`license_plate`),
  KEY `fk_vehicles_users_createdby` (`created_by`),
  CONSTRAINT `fk_vehicles_users_createdby` FOREIGN KEY (`created_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicles`
--

LOCK TABLES `vehicles` WRITE;
/*!40000 ALTER TABLE `vehicles` DISABLE KEYS */;
INSERT INTO `vehicles` VALUES (1,'1233333','cargo','ford',0,6,'2025-07-11 00:15:26'),(2,'12332','tektor','iveco',1,6,'2025-07-11 14:54:26'),(3,'1223','113H','Scania',1,5,'2025-07-12 17:22:35');
/*!40000 ALTER TABLE `vehicles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-19 19:28:50
