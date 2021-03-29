export class BuildingBase {
  name: string;
  startDate: Date;
  endDate: Date;
  durationInDays: number;
  fullPrice: number;

  //work
  hasToBeDoneAfterId: number;
  hasToBeDoneAfterName: string;

  executeByContractor: boolean;
  contractorName: string;
  contractorPrice: string;
  contractorExtraPrice: string;

  price: number;
  extraPrice: number;

  extraPricePercent: number;

  contractorFullPrice: number;
  resourcesFullPrice: number;
  resourcesPrice: number;

  priceForBMaterials: number;
  priceForCMaterials: number;
  priceForMMaterials: number;
  priceForTechnics: number;
  priceForBrigades: number;
  priceForWorkers: number;
}
