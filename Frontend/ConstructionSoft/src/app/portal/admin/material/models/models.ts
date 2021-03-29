export class MaterialDimension {
  id: number;
  name: string;
}

export class BuildingMaterialCategory {
  id: number;
  name: string;
}

export class BuildingMaterial {
  id:number;
  name: string;
  price: number;
  dimensionId: number;
  dimension: MaterialDimension;
  categoryId: number;
  category: BuildingMaterialCategory;
}

export class ConsumptionMaterialCategory {
  id: number;
  name: string;
}

export class ConsumptionMaterial {
  id:number;
  name: string;
  price: number;
  dimensionId: number;
  dimension: MaterialDimension;
  categoryId: number;
  category: ConsumptionMaterialCategory;
}

export class MainMaterialCategory {
  id: number;
  name: string;
}

export class MainMaterial {
  id:number;
  name: string;
  price: number;
  dimensionId: number;
  dimension: MaterialDimension;
  categoryId: number;
  category: MainMaterialCategory;
  depreciation: number;
}
