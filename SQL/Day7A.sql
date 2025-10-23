-- Healthcare System Schema

CREATE TABLE Patients (
    PatientID INT PRIMARY KEY,
    PatientName VARCHAR(100),
    DateOfBirth DATE,
    Gender VARCHAR(10)
);

CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY,
    DoctorName VARCHAR(100),
    Specialty VARCHAR(100)
);

CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY,
    PatientID INT,
    DoctorID INT,
    AppointmentDate DATE,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

CREATE TABLE Medications (
    MedicationID INT PRIMARY KEY,
    MedicationName VARCHAR(200),
    DosageForm VARCHAR(50)
);

CREATE TABLE Prescriptions (
    PrescriptionID INT PRIMARY KEY,
    PatientID INT,
    MedicationID INT,
    PrescriptionDate DATE,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (MedicationID) REFERENCES Medications(MedicationID)
);

-- Questions

-- 1. List all patients and their appointments, including patients who have never had an appointment.

select p.PatientID, p.PatientName, a.AppointmentID, a.AppointmentDate
from Patients p
left join Appointments a on p.PatientID = a.PatientID;

-- 2. Display all doctors and their scheduled appointments, including doctors without any appointments.

select d.DoctorID, d.DoctorName, a.AppointmentID, a.AppointmentDate
from Doctors d
left join Appointments a on d.DoctorID = a.DoctorID;

-- 3. Show all medications and the patients they've been prescribed to, including medications that haven't been prescribed.

select m.MedicationID, m.MedicationName, p.PatientID, p.PatientName
from Medications m
left join Prescriptions r on m.MedicationID = r.MedicationID
left join Patients p on r.PatientID = p.PatientID;

-- 4. List all possible patient-doctor combinations, regardless of whether an appointment has occurred.
select p.PatientID, p.PatientName, d.DoctorID, d.DoctorName
from Patients p
cross join Doctors d;

-- 5. Display all prescriptions with patient and medication information, including prescriptions where either the patient or medication information is missing.
select P.PrescriptionID, P.PrescriptionDate, Pa.PatientID, Pa.PatientName, M.MedicationID, M.MedicationName
from Prescriptions P
left join Patients Pa on P.PatientID = Pa.PatientID
left join Medications M on M.MedicationID = P.MedicationID;



-- 6. Show all patients who have never been prescribed any medication, along with their appointment history.
select p.PatientID, p.PatientName, a.AppointmentID, a.AppointmentDate
from Patients p
left join Prescriptions r on p.PatientID = r.PatientID
left join Appointments a on p.PatientID = a.PatientID
where r.PrescriptionID is null;

-- 7. List all doctors who have appointments in the next week, along with the patients they're scheduled to see.
	
		Select D.DoctorID,D.DoctorName,P.PatientID,P.PatientName 
		from Doctors D
		inner Join Appointments A ON D.DoctorID= A.DoctorID
		inner Join Patients P on P.PatientID= A.PatientID
		where A.AppointmentDate BETWEEN CAST(GETDATE() AS DATE) AND DATEADD(day, 7, CAST(GETDATE() AS DATE));
	

-- 8. Display all medications prescribed to patients over 60 years old, including medications not prescribed to this age group.

	select M.MedicationID,M.MedicationName 
	from Medications M
	left join Prescriptions P On M.MedicationID = P.MedicationID
	left Join Patients Pa On Pa.PatientID = P.PatientID
	AND DATEDIFF(YEAR, Pa.DateOfBirth, GETDATE()) > 60;

-- 9. Show all appointments from last year and any associated prescription information.

-- 10. List all possible specialty-medication combinations, regardless of whether a doctor of that specialty has prescribed that medication.
