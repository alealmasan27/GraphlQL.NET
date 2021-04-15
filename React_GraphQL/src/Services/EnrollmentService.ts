import { Enrolment } from "../Models/Enrolment";
import { Student } from "../Models/Student";

export class EnrollmentService {
    public static toEnrolment(enrolment: any): Enrolment {
        return {
          Id: enrolment.id,
          CourseId: enrolment.courseId,
          Grade: enrolment.grade,
          Student: this.toStudent(enrolment.student),
          StudentId: enrolment.studentId
        }
    }

    public static toStudent(student: any): Student {
        return {
          Id: student.id,
          FirstName: student.firstName,
          LastName: student.lastName,
          EnrolmentDate: student.enrollmentDate
        }
    }
}