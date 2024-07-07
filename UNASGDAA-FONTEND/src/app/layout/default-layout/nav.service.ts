import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { INavData } from '@coreui/angular';

@Injectable({
  providedIn: 'root'
})
export class NavService {
  constructor(private translate: TranslateService) {}

  getNavItems(): INavData[] {
    return [
      {
        name: this.translate.instant('SGAA'),
        url: '/dashboard',
        iconComponent: { name: 'cil-Calendar' },
        badge: {
          color: 'info',
          text: 'FIIS-UNAS'
        }
      },
      {
        title: true,
        name: this.translate.instant('Asesorados')
      },
      {
        name: this.translate.instant('Solicitudes'),
        url: '/advisory-contracts',
        iconComponent: { name: 'cilList' }
      },
      {
        name: this.translate.instant('Aceptados'),
        url: '/theme/typography',
        linkProps: { fragment: 'headings' },
        iconComponent: { name: 'cilSave' }
      },
      {
        name: this.translate.instant('Segumiento'),
        title: true
      },
      {
        name: this.translate.instant('Estudiantes'),
        url: '/base',
        iconComponent: { name: 'cilPeople' },
        children: [
          {
            name: this.translate.instant('Accordion'),
            url: '/base/accordion',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Breadcrumbs'),
            url: '/base/breadcrumbs',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Cards'),
            url: '/base/cards',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Carousel'),
            url: '/base/carousel',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Collapse'),
            url: '/base/collapse',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('List Group'),
            url: '/base/list-group',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Navs & Tabs'),
            url: '/base/navs',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Pagination'),
            url: '/base/pagination',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Placeholder'),
            url: '/base/placeholder',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Popovers'),
            url: '/base/popovers',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Progress'),
            url: '/base/progress',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Spinners'),
            url: '/base/spinners',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Tables'),
            url: '/base/tables',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Tabs'),
            url: '/base/tabs',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Tooltips'),
            url: '/base/tooltips',
            icon: 'nav-icon-bullet'
          }
        ]
      },
      {
        name: this.translate.instant('Contratos de Asesoria'),
        url: '/buttons',
        iconComponent: { name: 'cil-cursor' },
        children: [
          {
            name: this.translate.instant('Buttons'),
            url: '/buttons/buttons',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Button groups'),
            url: '/buttons/button-groups',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Dropdowns'),
            url: '/buttons/dropdowns',
            icon: 'nav-icon-bullet'
          }
        ]
      },
      {
        name: this.translate.instant('Citas'),
        url: '/forms',
        iconComponent: { name: 'cil-notes' },
        children: [
          {
            name: this.translate.instant('Form Control'),
            url: '/forms/form-control',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Select'),
            url: '/forms/select',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Checks & Radios'),
            url: '/forms/checks-radios',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Range'),
            url: '/forms/range',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Input Group'),
            url: '/forms/input-group',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Floating Labels'),
            url: '/forms/floating-labels',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Layout'),
            url: '/forms/layout',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Validation'),
            url: '/forms/validation',
            icon: 'nav-icon-bullet'
          }
        ]
      },
      {
        name: this.translate.instant('Calendario'),
        iconComponent: { name: 'cil-calendar' },
        url: '/charts'
      },
      {
        name: this.translate.instant('Profesores'),
        iconComponent: { name: 'cil-calculator' },
        url: '/icons',
        children: [
          {
            name: this.translate.instant('CoreUI Free'),
            url: '/icons/coreui-icons',
            icon: 'nav-icon-bullet',
            badge: {
              color: 'success',
              text: 'FREE'
            }
          },
          {
            name: this.translate.instant('CoreUI Flags'),
            url: '/icons/flags',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('CoreUI Brands'),
            url: '/icons/brands',
            icon: 'nav-icon-bullet'
          }
        ]
      },
      {
        name: this.translate.instant('Notificaciones'),
        url: '/notifications',
        iconComponent: { name: 'cil-bell' },
        children: [
          {
            name: this.translate.instant('Alerts'),
            url: '/notifications/alerts',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Badges'),
            url: '/notifications/badges',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Modal'),
            url: '/notifications/modal',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Toast'),
            url: '/notifications/toasts',
            icon: 'nav-icon-bullet'
          }
        ]
      },
      {
        name: this.translate.instant('Reportes'),
        url: '/widgets',
        iconComponent: { name: 'cilSpeedometer' },
        badge: {
          color: 'info',
          text: 'NEW'
        }
      },
      {
        name: this.translate.instant('Logros'),
        url: '/widgets',
        iconComponent: { name: 'cil-star' },
        badge: {
          color: 'info',
          text: 'NEW'
        }
      },
      {
        title: true,
        name: this.translate.instant('Configuraciones')
      },
      {
        name: this.translate.instant('Pages'),
        url: '/login',
        iconComponent: { name: 'cilSettings' },
        children: [
          {
            name: this.translate.instant('Login'),
            url: '/login',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Register'),
            url: '/register',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Error 404'),
            url: '/404',
            icon: 'nav-icon-bullet'
          },
          {
            name: this.translate.instant('Error 500'),
            url: '/500',
            icon: 'nav-icon-bullet'
          }
        ]
      },
      {
        title: true,
        name: this.translate.instant('Links'),
        class: 'mt-auto'
      },
      {
        name: this.translate.instant('Reportes'),
        url: 'https://coreui.io/angular/docs/5.x/',
        iconComponent: { name: 'cil-description' },
        attributes: { target: '_blank' }
      }
    ];
  }
}
