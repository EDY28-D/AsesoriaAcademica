import { Routes } from '@angular/router';

export const routes: Routes = [
    
        {
            path: '',
            loadComponent: () => import('./advisory-contract.component').then(m => m.AdvisoryContractComponent),
            data: {
              title: $localize`advisory-contracts`
            }
          }
    
  ];
  
  