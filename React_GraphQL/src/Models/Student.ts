import { Enrolment } from "./Enrolment";

export interface Student {
    Id?: number;
    LastName: string;
    FirstName: string;
    EnrolmentDate: Date;
    Enrolments?: Enrolment[];
}