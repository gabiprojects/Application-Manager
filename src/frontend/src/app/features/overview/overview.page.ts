import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApplicationStatus } from '../../generated/api';
import { CompanyService } from '../companies/company.service';
import { JobApplicationService } from '../job-applications/job-application.service';

@Component({
  selector: 'app-overview-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="space-y-6">
      <div class="flex items-center justify-between">
        <div>
          <p class="text-sm font-medium text-sky-700">Overview</p>
          <h1 class="text-3xl font-bold text-slate-900">Dashboard</h1>
        </div>
      </div>

      <div class="grid gap-4 md:grid-cols-3">
        <div class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p class="text-sm text-slate-500">Companies</p>
          <p class="mt-3 text-3xl font-bold text-slate-900">{{ companyService.companyCount() }}</p>
        </div>
        <div class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p class="text-sm text-slate-500">Applications</p>
          <p class="mt-3 text-3xl font-bold text-slate-900">{{ jobApplicationService.applicationCount() }}</p>
        </div>
        <div class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p class="text-sm text-slate-500">Interview pipeline</p>
          <p class="mt-3 text-3xl font-bold text-slate-900">{{ interviewCount() }}</p>
        </div>
      </div>

      <div class="grid gap-6 lg:grid-cols-[1.4fr_0.9fr]">
        <section class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <div class="mb-4 flex items-center justify-between">
            <h2 class="text-xl font-semibold text-slate-900">Recent applications</h2>
            <a routerLink="/job-applications" class="text-sm font-medium text-sky-700 hover:text-sky-800">View all</a>
          </div>

          <div *ngIf="jobApplicationService.loading(); else recentApplications" class="text-sm text-slate-500">Loading...</div>

          <ng-template #recentApplications>
            <div class="space-y-3">
              <div *ngFor="let application of recentApplicationsList()" class="flex items-center justify-between rounded-xl border border-slate-200 p-3">
                <div>
                  <p class="font-semibold text-slate-900">{{ application.positionTitle }}</p>
                  <p class="text-sm text-slate-500">{{ application.companyName }}</p>
                </div>
                <span class="rounded-full bg-sky-100 px-2.5 py-1 text-xs font-medium text-sky-700">
                  {{ application.status }}
                </span>
              </div>
            </div>
          </ng-template>
        </section>

        <aside class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <h2 class="text-xl font-semibold text-slate-900">Status overview</h2>
          <div class="mt-4 space-y-3">
            <div *ngFor="let status of statusEntries()" class="flex items-center justify-between">
              <span class="text-sm uppercase tracking-wide text-slate-500">{{ status.key }}</span>
              <span class="rounded-full bg-slate-100 px-2 py-1 text-xs font-medium text-slate-700">{{ status.value }}</span>
            </div>
          </div>
        </aside>
      </div>
    </div>
  `,
})
export class OverviewPage implements OnInit {
  protected readonly companyService = inject(CompanyService);
  protected readonly jobApplicationService = inject(JobApplicationService);

  readonly statusEntries = computed(() => {
    const counts = this.jobApplicationService.statusCounts();
    return Array.from(counts.entries()).map(([key, value]) => ({ key, value }));
  });

  readonly recentApplicationsList = computed(() => {
    return [...this.jobApplicationService.applications()]
      .sort((a, b) => new Date(b.appliedOn ?? 0).getTime() - new Date(a.appliedOn ?? 0).getTime())
      .slice(0, 5);
  });

  readonly interviewCount = computed(() => {
    const apps = this.jobApplicationService.applications();
    return apps.filter((app) => app.status === ApplicationStatus.Interviewing || app.status === ApplicationStatus.Hired).length;
  });

  ngOnInit(): void {
    this.companyService.loadCompanies();
    this.jobApplicationService.loadApplications();
  }
}
