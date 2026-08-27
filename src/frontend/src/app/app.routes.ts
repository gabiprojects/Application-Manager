import { Routes } from '@angular/router';
import { AppShellComponent } from './core/layout/app-shell.component';
import { OverviewPage } from './features/overview/overview.page';
import { CompanyListPage } from './features/companies/company-list.page';
import { CompanyDetailPage } from './features/companies/company-detail.page';
import { JobApplicationListPage } from './features/job-applications/job-application-list.page';
import { JobApplicationDetailPage } from './features/job-applications/job-application-detail.page';
import { JobApplicationNewPage } from './features/job-applications/job-application-new.page';

export const routes: Routes = [
  {
    path: '',
    component: AppShellComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: OverviewPage },
      { path: 'companies', component: CompanyListPage },
      { path: 'companies/:id', component: CompanyDetailPage },
      { path: 'job-applications', component: JobApplicationListPage },
      { path: 'job-applications/new', component: JobApplicationNewPage },
      { path: 'job-applications/:id', component: JobApplicationDetailPage },
    ],
  },
];
