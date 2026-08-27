import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { JobApplicationService } from './job-application.service';

@Component({
  selector: 'app-job-application-list-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="space-y-6">
      <div class="flex items-center justify-between">
        <div>
          <p class="text-sm font-medium text-sky-700">Job applications</p>
          <h1 class="text-3xl font-bold text-slate-900">Applications</h1>
        </div>
        <a routerLink="/job-applications/new" class="rounded-xl bg-sky-600 px-4 py-2 text-sm font-medium text-white hover:bg-sky-700">New application</a>
      </div>

      <div *ngIf="jobApplicationService.loading(); else list" class="text-sm text-slate-500">Loading applications...</div>

      <ng-template #list>
        <div class="space-y-3">
          <a *ngFor="let application of jobApplicationService.applications()" [routerLink]="['/job-applications', application.id]" class="block rounded-2xl border border-slate-200 bg-white p-4 shadow-sm transition hover:-translate-y-0.5 hover:shadow-md">
            <div class="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
              <div>
                <h2 class="text-lg font-semibold text-slate-900">{{ application.positionTitle }}</h2>
                <p class="text-sm text-slate-500">{{ application.companyName }}</p>
              </div>
              <span class="inline-flex rounded-full bg-sky-100 px-2.5 py-1 text-xs font-medium text-sky-700">{{ application.status }}</span>
            </div>
          </a>
        </div>
      </ng-template>
    </div>
  `,
})
export class JobApplicationListPage implements OnInit {
  protected readonly jobApplicationService = inject(JobApplicationService);

  ngOnInit(): void {
    this.jobApplicationService.loadApplications();
  }
}
