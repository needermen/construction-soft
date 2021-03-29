export class TechnicDimension {
  id: number;
  name: string;
}

export class TechnicCategory {
  id: number;
  name: string;
}

export class Technic {
  id:number;
  name: string;
  price: number;
  dimensionId: number;
  dimension: TechnicDimension;
  categoryId: number;
  category: TechnicCategory;
}
