DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `create_movement`(
IN p_name VARCHAR(255),
IN p_id VARCHAR(36),
    IN p_createdAt DATETIME,
    IN p_createdBy VARCHAR(255),
    IN p_updatedAt DATETIME,
    IN p_updatedBy VARCHAR(255),
    IN p_muscleGroup VARCHAR(10)
    )
BEGIN
INSERT INTO Movement (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, MuscleGroup)
    VALUES (p_id ,p_name, p_createdAt, p_createdBy, p_updatedAt, p_updatedBy, p_muscleGroup);
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `create_user`(
IN p_username VARCHAR(255),
IN p_id VARCHAR(36),
    IN p_email VARCHAR(255),
    IN p_createdAt DATETIME,
    IN p_createdBy VARCHAR(255),
    IN p_updatedAt DATETIME,
    IN p_updatedBy VARCHAR(255),
    IN p_password VARCHAR(255),
    IN p_role VARCHAR(10)
)
BEGIN
	INSERT INTO User (Id, Username, Email, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, Password, Role)
    VALUES (p_id ,p_username, p_email, p_createdAt, p_createdBy, p_updatedAt, p_updatedBy, p_password, p_role);
END$$
DELIMITER ;

----------------------------------------------------------------
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `create_workout`(
	IN p_id VARCHAR(255),
    IN p_level VARCHAR(10),
    IN p_duration VARCHAR(10),
    IN p_name VARCHAR(255),
    IN p_movementIds VARCHAR(255),  -- Assuming movementIds is a comma-separated list
    IN p_createdBy VARCHAR(255),
    IN p_createdAt DATETIME,
    IN p_updatedBy VARCHAR(255),
    IN p_updatedAt DATETIME
)
BEGIN
    DECLARE lastInsertedId INT;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
          ROLLBACK;
          SELECT SQLEXCEPTON;
    END;
    
    START TRANSACTION;

    -- Insert into workout table
    INSERT INTO Workout (Id, Level, Duration, Name,  CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES (p_id, p_level, p_duration, p_name, p_createdBy, p_createdAt, p_updatedBy, p_updatedAt);

    -- Get the last inserted workout ID
    SET lastInsertedId = LAST_INSERT_ID();

    -- Insert into WorkoutMovements based on MovementIds
    INSERT INTO WorkoutMovements (workoutId, movementId)
    SELECT p_id, Id
    FROM Movement
    WHERE FIND_IN_SET(Id, p_movementIds) > 0;
    
    COMMIT;

    
END$$
DELIMITER ;

----------------------------------------------------------------


DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_movement`(
	IN p_id VARCHAR(255)
)
BEGIN

DELETE FROM Movement WHERE Id = p_id;
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_workout`(
	IN p_id VARCHAR(255)
)
BEGIN

DELETE FROM Workout WHERE Id = p_id;
END$$
DELIMITER ;


----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_all_movements`()
BEGIN
SELECT * FROM MOVEMENT;
END$$
DELIMITER ;

----------------------------------------------------------------
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_movement_by_id`(IN p_id VARCHAR(255)
)
BEGIN
SELECT * FROM Movement WHERE Id = p_id;
END$$
DELIMITER ;
----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_movement_by_name`(IN p_name VARCHAR(255)
)
BEGIN
SELECT * FROM Movement WHERE Name = p_name;
END$$
DELIMITER ;

----------------------------------------------------------------


DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_user_by_email`(
IN p_email VARCHAR(255)
)
BEGIN
SELECT * FROM USER WHERE Email = p_email;
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_workout_by_id`(IN p_id VARCHAR(255)
)
BEGIN
SELECT * FROM Workout WHERE Id = p_id;
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_workout_by_name`(IN p_name VARCHAR(255)
)
BEGIN
SELECT * FROM Workout WHERE Name = p_name;
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_workout_movements`(IN p_id VARCHAR(255)
)
BEGIN
SELECT m.* FROM WorkoutMovements wm inner join Movement m on (m.id = wm.MovementId) WHERE wm.WorkoutId = p_id;
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_workouts`(
	IN p_level VARCHAR(10),
    IN p_duration VARCHAR(10),
    IN p_muscleGroup VARCHAR(10)
)
BEGIN
SELECT * FROM Workout w inner join WorkoutMovements wm on (wm.WorkoutId = w.Id)
inner join Movement m on (wm.MovementId = m.Id)
 Where 
	(w.Level = p_level OR p_level is NULL) AND
    (w.Duration = p_duration OR p_duration is NULL) AND
    (m.MuscleGroup = p_muscleGroup OR p_muscleGroup is NULL);
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_movement`(IN p_id VARCHAR(255),
IN p_name VARCHAR(255),
     IN p_createdAt DATETIME,
     IN p_createdBy VARCHAR(255),
     IN p_muscleGroup VARCHAR(255),
     IN p_updatedAt DATETIME,
     IN p_updatedBy VARCHAR(255))
BEGIN
UPDATE Movement
    SET 
    Name = p_name,
        CreatedAt = p_createdAt,
        CreatedBy = p_createdBy,
        MuscleGroup = p_muscleGroup,
        UpdatedAt = p_updatedAt,
        UpdatedBy = p_updatedBy
    WHERE Id = p_id;
END$$
DELIMITER ;

----------------------------------------------------------------

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_workout`(
	IN p_id VARCHAR(255),
    IN p_level VARCHAR(10),
    IN p_duration VARCHAR(10),
    IN p_name VARCHAR(255),
    IN p_movementIds VARCHAR(255),  -- Assuming movementIds is a comma-separated list
    IN p_updatedBy VARCHAR(255),
    IN p_updatedAt DATETIME
)
proc :BEGIN
    DECLARE lastInsertedId INT;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
          ROLLBACK;
          SELECT SQLEXCEPTON;
    END;
    
    START TRANSACTION;

    UPDATE Workout
        SET
            Level = p_level,
            Duration = p_duration,
            Name = p_name,
            UpdatedBy = p_updatedBy,
            UpdatedAt = p_updatedAt
        WHERE Id = p_id;

        -- Delete existing workout-movement associations
		IF p_movementIds IS NULL THEN LEAVE proc; END IF;
        DELETE FROM WorkoutMovements WHERE workoutId = p_id;

        -- Insert into WorkoutMovements based on MovementIds
        INSERT INTO WorkoutMovements (workoutId, movementId)
        SELECT p_id, Id
        FROM Movement
        WHERE FIND_IN_SET(Id, p_movementIds) > 0;
    
    COMMIT;

    
END$$
DELIMITER ;
