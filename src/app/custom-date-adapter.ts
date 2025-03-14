import { NativeDateAdapter } from '@angular/material/core';

export class CustomDateAdapter extends NativeDateAdapter {
//   override format(date: Date, displayFormat: Object): string {
//     if (displayFormat === 'input') {
//       const month = date.getMonth() + 1;
//       const year = date.getFullYear();
//       return `${month}/${year}`;
//     }
//     return date.toDateString();
//   }

//   override parse(value: any): Date | null {
//     const [month, year] = value.split('/').map((val: string) => +val);
//     return new Date(year, month - 1, 1);
//   }
}


