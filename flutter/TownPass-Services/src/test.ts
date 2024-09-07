// 嬰兒接口
export interface IBaby {
  birthday: number; // 年齡
  birthOrder: number; // 第幾胎
  height: number; // 身高
  weight: number; // 體重
  isLowIncomeHousehold: boolean; // 中低收入戶
  isIndigenous: boolean; // 原住民
  //careType: ICareType; // 托嬰類別
  //vaccineTracking: IVaccineTracking[]; // 疫苗追蹤
  //subsidy: ISubsidy; // 補助
}

// 補助接口
export interface ISubsidy {
  birthOrderSubsidy: IBirthOrderSubsidy; // 每幾胎補助
  daycareSubsidy: IDaycareSubsidy; // 托嬰補助
}

// 托嬰類別接口
export interface ICareType {
  caregiver: 1 | 2 |3 | 4; // 1: 自己, 2: 保母 3.托嬰 4.幼兒園
  Category:ICategory ;
}

//
export interface ICategory {
  caregiver: number; 
  CategoryId: number; 
  CategoryName: string; 
}
// 疫苗追蹤接口
export interface IVaccineTracking {
  vaccineNameCN: string; // 疫苗中文名稱
  vaccineNameEN: string; // 疫苗英文簡稱
  vaccinationSchedule: string; // 接種時程
  dose: number; // 劑次
  contraindications: string; // 接種禁忌
  vaccinated: boolean; // 是否已接種
}

// 補助明細接口
export interface ISubsidyDetail {
  amount: number; // 金額
  unit: string; // 單位
}

// 每幾胎補助接口
export interface IBirthOrderSubsidy {
  totalAmount: number; // 金額總額
  details: ISubsidyDetail[]; // 補助明細陣列
}

// 托嬰補助接口
export interface IDaycareSubsidy {
  totalAmount: number; // 金額總額
  details: ISubsidyDetail[]; // 補助明細陣列
}
