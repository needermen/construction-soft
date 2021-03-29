export class PaymentType {
  id: number;
  name: string;
}

export class WorkerCategory {
  id: number;
  name: string;
}

export class Worker {
  id:number;
  name: string;
  salary: number;
  paymentTypeId: number;
  paymentTypeName: string;
  categoryId: number;
  categoryName: string;
}

export class BrigadeCategory {
  id: number;
  name: string;
}

export class Brigade {
  id:number;
  name: string;
  salary: number;
  paymentTypeId: number;
  paymentTypeName: string;
  categoryId: number;
  categoryName: string;
  files: File[];
}

export class File {
  id: number;
  fileName: number;
}
