
// 嬰兒 Class
class Baby {
  age: number; // 年齡
  birthOrder: number; // 第幾胎
  height: number; // 身高
  weight: number; // 體重
  isLowIncomeHousehold: boolean; // 中低收入戶
  isIndigenous: boolean; // 原住民
  careType: CareType; // 托嬰類別
  vaccineTracking: VaccineTracking[]; // 疫苗追蹤
  subsidy: Subsidy; // 補助

  constructor(
    age: number,
    birthOrder: number,
    height: number,
    weight: number,
    isLowIncomeHousehold: boolean,
    isIndigenous: boolean,
    careType: CareType,
    vaccineTracking: VaccineTracking[],
    subsidy: Subsidy
  ) {
    this.age = age;
    this.birthOrder = birthOrder;
    this.height = height;
    this.weight = weight;
    this.isLowIncomeHousehold = isLowIncomeHousehold;
    this.isIndigenous = isIndigenous;
    this.careType = careType;
    this.vaccineTracking = vaccineTracking;
    this.subsidy = subsidy;
  }
}

// 補助
class Subsidy {
  birthOrderSubsidy: BirthOrderSubsidy; // 每幾胎補助
  daycareSubsidy: DaycareSubsidy; // 托嬰補助

  constructor(birthOrderSubsidy: BirthOrderSubsidy, daycareSubsidy: DaycareSubsidy) {
    this.birthOrderSubsidy = birthOrderSubsidy;
    this.daycareSubsidy = daycareSubsidy;
  }
}


// 托嬰類別
class CareType {
  caregiver: 1 | 2; // 1: 自己, 2: 保母
  hasCertificate: boolean; // 是否有證照
  isGrandparent: boolean; // 是否是隔代

  constructor(caregiver: 1 | 2, hasCertificate: boolean, isGrandparent: boolean) {
    this.caregiver = caregiver;
    this.hasCertificate = hasCertificate;
    this.isGrandparent = isGrandparent;
  }
}

// 疫苗追蹤
class VaccineTracking {
  vaccineNameCN: string; // 疫苗中文名稱
  vaccineNameEN: string; // 疫苗英文簡稱
  vaccinationSchedule: string; // 接種時程
  dose: number; // 劑次
  contraindications: string; // 接種禁忌
  vaccinated: boolean; // 是否已接種

  constructor(
    vaccineNameCN: string,
    vaccineNameEN: string,
    vaccinationSchedule: string,
    dose: number,
    contraindications: string,
    vaccinated: boolean
  ) {
    this.vaccineNameCN = vaccineNameCN;
    this.vaccineNameEN = vaccineNameEN;
    this.vaccinationSchedule = vaccinationSchedule;
    this.dose = dose;
    this.contraindications = contraindications;
    this.vaccinated = vaccinated;
  }
}

// 補助明細
class SubsidyDetail {
  amount: number; // 金額
  unit: string; // 單位

  constructor(amount: number, unit: string) {
    this.amount = amount;
    this.unit = unit;
  }
}

// 每幾胎補助
class BirthOrderSubsidy {
  totalAmount: number; // 金額總額
  details: SubsidyDetail[]; // 補助明細陣列

  constructor(totalAmount: number, details: SubsidyDetail[]) {
    this.totalAmount = totalAmount;
    this.details = details;
  }
}

// 托嬰補助
class DaycareSubsidy {
  totalAmount: number; // 金額總額
  details: SubsidyDetail[]; // 補助明細陣列

  constructor(totalAmount: number, details: SubsidyDetail[]) {
    this.totalAmount = totalAmount;
    this.details = details;
  }
}

