import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from './icons/icon-subset';
import { AdvisoryContractComponent } from './advisory-contract/advisory-contract.component';


@Component({
  selector: 'app-root',
  template: `
 <div style="display: flex; gap: 10px;">
      fadsffadsddfgdfhgfadsfasfdasdft<button style="font-size: 1.2em; display: flex; align-items: center; gap: 5px; color: #007bff; background-color: transparent; border: none; cursor: pointer;">
        <i class="fas fa-flag-usa"></i> 
      </button>
      <button  style="font-size: 1.2em; display: flex; align-items: center; gap: 5px; color: #007bff; background-color: transparent; border: none; cursor: pointer;">
        <i class="fas fa-flag"></i>English
      </button>
    </div>
    <router-outlet></router-outlet>

    <router-outlet></router-outlet>
  `,
  standalone: true,
  imports: [RouterOutlet, AdvisoryContractComponent]
})
export class AppComponent implements OnInit {
  title = 'Sistema Gestion de Asesorados';

  constructor(
    private router: Router,
    private titleService: Title,
    private translate: TranslateService,
    private iconSetService: IconSetService
  ) {
    this.titleService.setTitle(this.title);

    this.translate.use('en');
    this.iconSetService.icons = { ...iconSubset };
  }

  ngOnInit(): void {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
  ///proxi 
    });
  }
 
}
 

