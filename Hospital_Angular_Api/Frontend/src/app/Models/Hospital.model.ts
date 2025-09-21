import { Doctors } from "./Doctor.model";
import { Patient } from "./Patient.model";

export interface Hospital {
hospitalId: string;
name?: string | null;
doctors?: Doctors[] | null;
patients?: Patient[] | null;
}