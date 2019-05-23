-- Lecturer : How do I update a specific read to only a specific column? *** solved 
SELECT *
FROM Announcement
INNER JOIN overseasEnrolledLecturer on overseasEnrolledLecturer.tripID=announcement.tripID;

UPDATE Announcement
SET announcementRead='Read'
FROM announcement 
INNER JOIN overseasEnrolledLecturer on overseasEnrolledLecturer.tripID=announcement.tripID
WHERE overseasEnrolledLecturer.staffID='johnny_appleseed' and announcementID=39;
/*UPDATE Announcement
SET announcementRead='Unread'
FROM announcement*/

-- Student
/*SELECT COUNT(*) AS unreadCount
FROM Announcement
INNER JOIN overseasEnrolledStudent on overseasEnrolledStudent.tripID=announcement.tripID
WHERE overseasEnrolledStudent.adminNo='171846Z' and announcementRead='Unread';*/
/*UPDATE WITHDRAWTRIPREQUEST
SET withdrawalTripRequestStatus = 'Approved'
WHERE withdrawTripRequestID = 12;*/
