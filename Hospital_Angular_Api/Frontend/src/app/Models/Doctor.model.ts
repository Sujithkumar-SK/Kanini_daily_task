import { Hospital } from "./Hospital.model";
import { Patient } from "./Patient.model";

export interface Doctors {
doctorId: string;
name?: string | null;
specialization?: string | null;
hospitalId: string;
hospital?: Hospital | null;
patients?: Patient[] | null;
}