/*SELECT CONCAT(student.adminNo, ' is ','weird' ) FROM injuryReport
INNER JOIN overseasTrip on overseasTrip.tripID = injuryReport.tripID
INNER JOIN student on student.adminNo = injuryReport.adminNo
INNER JOIN lecturer on lecturer.staffID = injuryReport.staffID
INNER JOIN account on account.accountID = student.accountID
INNER JOIN account a on a.accountID = lecturer.accountID;
SELECT CONCAT('SQL', ' ', 'is', ' ', 'fun!');*/

--SELECT * FROM injuryReport;
--SELECT * FROM injuryReport where dateTimeOfInjury >= GETDATE();
--SELECT * FROM announcement;
--INNER JOIN overseasTrip on overseasTrip.tripID = injuryReport.tripID INNER JOIN lecturer on lecturer.staffID = injuryReport.staffID  INNER JOIN account on account.accountID = lecturer.accountID;
/*INNER JOIN overseasTrip on overseasTrip.tripID = injuryReport.tripID
INNER JOIN lecturer on lecturer.staffID = injuryReport.staffID 
INNER JOIN account on account.accountID = lecturer.accountID;*/

/*SELECT * FROM injuryReport
INNER JOIN overseasTrip on overseasTrip.tripID = injuryReport.tripID -- receive trip name
INNER JOIN lecturer on lecturer.staffID = injuryReport.staffID 
INNER JOIN account on account.accountID = lecturer.accountID; -- receive lecturer name*/
Select * From overseasTrip INNER JOIN overseasEnrolledStudent ON overseasTrip.tripID = overseasEnrolledStudent.tripID WHERE adminNo = '171846z' and tripType='Study Trip' 
--WHERE (overseasEnrolledLecturer.staffID = 'johnny_appleseed'  and tripType='Immersion Trip' and lecturerView='True') OR (createdBy= 'johnny_appleseed' and tripType='Immersion Trip' and lecturerView='False' and overseasEnrolledLecturer.staffID = 'johnny_appleseed');
/*SELECT * FROM announcement 
INNER JOIN overseasTrip on announcement.tripID = overseasTrip.tripID 
INNER JOIN overseasEnrolledLecturer on overseasEnrolledLecturer.tripID = overseasTrip.tripID 
INNER JOIN lecturer on announcement.staffID = lecturer.staffID 
INNER JOIN account on account.accountID = lecturer.accountID 
WHERE createdBy= 'johnny_appleseed' and tripType='Immersion Trip' and lecturerView='False' and overseasEnrolledLecturer.staffID = 'johnny_appleseed';*/
