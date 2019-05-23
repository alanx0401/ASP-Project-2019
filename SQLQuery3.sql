select count(*) from  Announcement
INNER JOIN overseasTrip on announcement.tripID = overseasTrip.tripID
INNER JOIN overseasEnrolledLecturer on overseasEnrolledLecturer.tripID = overseasTrip.tripID
where
STR(announcementID)+'.'+'johnny_appleseed' not in (select STR(announcementID)+'.'+staffID from lecturerAnnRead) and overseasEnrolledLecturer.staffID='johnny_appleseed';

select * from  Announcement
INNER JOIN overseasTrip on announcement.tripID = overseasTrip.tripID
INNER JOIN overseasEnrolledStudent on overseasEnrolledStudent.tripID = overseasTrip.tripID
where
STR(announcementID)+'.'+'171846Z' not in (select STR(aID)+'.'+sID from studentAnnRead) and overseasEnrolledStudent.adminNo='171846z';

Select * From overseasTrip
INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID
WHERE adminNo = '171846Z' and tripType='Study Trip' and arrivalDate>=GETDATE()
ORDER BY arrivalDate DESC;


select * from withdrawTripRequest
--=====================================================================================================
select * from overseasTrip
INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID
where 
STR(overseasTrip.tripID)+'.'+'171846Z' 
not in (select STR(tripID)+'.'+adminNo from withdrawTripRequest where withdrawalTripRequestStatus='Approved')
and adminNo='171846Z'
and tripType='Study Trip'
and arrivalDate>=GETDATE()
ORDER BY arrivalDate DESC;
--=====================================================================================================
Select * From overseasTrip
INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID
WHERE adminNo = '171846Z' and tripType='Study Trip' and arrivalDate>=GETDATE()
ORDER BY arrivalDate DESC;
--=====================================================================================================

select * from  Announcement
where
STR(announcementID)+'.'+'171846Z' not in (select STR(aID)+'.'+sID from AnnRead)

--========================================================================================
SELECT * FROM student 
INNER JOIN account on student.accountID = account.accountID 
INNER JOIN overseasEnrolledStudent on overseasEnrolledStudent.adminNo = student.adminNo
INNER JOIN overseasTrip on overseasTrip.tripID = overseasEnrolledStudent.tripID
WHERE course='C85' and (departureDate> '02-14-07' and arrivalDate> '02-14-07') or (departureDate< '02-14-07' and arrivalDate< '02-14-07')

--========================================================================================


UPDATE overseasTrip
SET overseasTripStatus='ONGOING'
WHERE departureDate<= GETDATE() AND arrivalDate>=GETDATE();

SELECT * FROM overseasTrip;


UPDATE overseasTrip
SET overseasTripStatus='ENDED'
WHERE departureDate<= GETDATE() AND arrivalDate<=GETDATE();

SELECT * FROM overseasTrip
INNER JOIN overseasEnrolledLecturer ON overseasTrip.tripID = overseasEnrolledLecturer.tripID
WHERE staffID='johnny_appleseed' and tripType='Internship' and overseasTripStatus='ENDED'
ORDER BY arrivalDate DESC;

SELECT overseasTrip.tripID, tripName, CONCAT(tripName,' (', tripType, ')') AS tripNameAndTripType
FROM overseasTrip 
INNER JOIN overseasEnrolledStudent on overseasTrip.tripID = overseasEnrolledStudent.tripID 
WHERE adminNo='171846Z' and overseasTripStatus!='PENDING';