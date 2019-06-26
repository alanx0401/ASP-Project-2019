SELECT * 
FROM EventLogs 

SELECT COUNT(eventDesc) as EventCount, UUID
FROM Eventlogs
WHERE eventDesc='Incorrect Password'
GROUP BY UUID;

