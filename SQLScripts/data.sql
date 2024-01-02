CREATE TABLE `Movement` (
  `Id` varchar(36) NOT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `CreatedBy` varchar(36) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `UpdatedBy` varchar(36) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `MuscleGroup` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ;

CREATE TABLE `User` (
  `Id` varchar(36) NOT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `CreatedBy` varchar(36) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `UpdatedBy` varchar(36) DEFAULT NULL,
  `Username` varchar(45) DEFAULT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Role` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) 

CREATE TABLE `Workout` (
  `Id` varchar(36) NOT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `CreatedBy` varchar(36) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `UpdatedBy` varchar(36) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Level` varchar(10) DEFAULT NULL,
  `Duration` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`Id`)
)

CREATE TABLE `WorkoutMovements` (
  `WorkoutId` varchar(36) NOT NULL,
  `MovementId` varchar(36) NOT NULL,
  PRIMARY KEY (`WorkoutId`,`MovementId`),
  KEY `workoutmovements_ibfk_2` (`MovementId`),
  CONSTRAINT `workoutmovements_ibfk_1` FOREIGN KEY (`WorkoutId`) REFERENCES `Workout` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `workoutmovements_ibfk_2` FOREIGN KEY (`MovementId`) REFERENCES `Movement` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
)