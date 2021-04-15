import { Course } from './Course';
import { Grade } from './Grade';
import { Student } from './Student';

export interface Enrolment {
    Id?: number;
    CourseId?: number;
    StudentId?: number;
    Grade?: Grade;
    Student: Student;
    Course?: Course;
}