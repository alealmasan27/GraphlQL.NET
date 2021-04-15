import { Course } from "../Models/Course";
import { Enrolment } from "../Models/Enrolment";
import { Student } from "../Models/Student";

export class StudentService {
    public static toDifferentStudent(student: any): Student {
        return {
          Id: student.id || -1,
          FirstName: student.firstName,
          LastName: student.lastName,
          EnrolmentDate: student.enrollmentDate,
          Enrolments: this.toDifferentEnrolments(student.enrollments)
        }
      }
    
    public static toDifferentEnrolments(enrolments: any): Enrolment[] {
        return enrolments.map((enrolment: any) => {
          return {
            Id: enrolment.id,
            CourseId: enrolment.courseId,
            Grade: enrolment.grade,
            StudentId: enrolment.studentId,
            Course: this.toCourse(enrolment.course)
          };
        });
      }
    
    public static toCourse(course: any): Course{
        return {
          Id: course.id,
          Title: course.title,
          Credits: course.credits
        }
      }
}