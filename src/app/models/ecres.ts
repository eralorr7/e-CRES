export interface Ecres {
    contractId: number;
    companyId: string;
    companyName: string;
    contractType: string;
    contractNo: string;
    contractDate: string;
    shipmentId: number;
    shipmentType: string;
    month1: string;
    month2: string;
    buyerSeller: string;
    rubberType: string;
    rubberId: string;
    remarksCentrifugedLatex: string;
    quantity: number | null;
    quantityActual: number | null;
    statusId : number

    unit : string;
    currency: string;   
    price: number | 'N/A'; // Allow `price` to be a number or 'N/A'
    priceEquivalent: number | 'N/A'; // Allow `priceEquivalent` to be a number or 'N/A'
    shipmentTermId: number;
    shipmentTerm: string;
    trade : string;

    otherTerm: string;
    placeFactoryPort: string;
    destination: string;
    createdDate: Date;
    updatedDate: Date;
    resubmitDate: Date;
    deletedStatus : Boolean;
    isDraft : Boolean;
    exportedCreatedDate?: string; // New property for export
    exportedContractDate?: string; // New property for export

    selectedMonth: number; 
    selectedYear: number;
  
    selectedMonth2: number;
    selectedYear2: number;
    isSubmitEnabled: boolean;

    rubberTypes: string[]; 
}