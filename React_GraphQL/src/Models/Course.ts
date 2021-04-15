import { Enrolment } from "./Enrolment";

export interface Course {
    Id?: number;
    Title?: string;
    Credits?: number;
    Enrollments?: Enrolment[];
}