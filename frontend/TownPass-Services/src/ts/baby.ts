export interface VaccineTracking {
  Vaccinated: number;
  Date: string;
  VaccineNameCN: string;
  VaccineNameEN: string;
  VaccinationSchedule: string;
  Dose: string;
  Contraindications: string;
}

export interface Subsidies {
  Amount: string;
  Details: any[]; // 根據具體需求，可以進一步定義 Details 的結構
}

export interface Baby {
  Age: string;
  BirthOrder: string;
  Height: string;
  Weight: string;
  LowIncomeHousehold: string;
  IsIndigenous: boolean;
  SubsidyName: string;
  Daycare: string;
  VaccineTracking: VaccineTracking[];
  Subsidies: Subsidies;
}
