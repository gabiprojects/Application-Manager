import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { JobApplicationService } from './job-application.service';

@Component({
  selector: 'app-job-application-detail-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div *ngIf="jobApplicationService.selectedApplication() as application; else loading" class="space-y-6">
      <div class="flex items-center justify-between">
        <div>
          <p class="text-sm font-medium text-sky-700">Application</p>
          <h1 class="text-3xl font-bold text-slate-900">{{ application.positionTitle }}</h1>
        </div>
        <a routerLink="/job-applications" class="rounded-xl border border-slate-200 bg-white px-4 py-2 text-sm font-medium text-slate-700 hover:bg-slate-50">Back</a>
      </div>

      <div class="grid gap-6 lg:grid-cols-[1.2fr_0.8fr]">
        <section class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <h2 class="text-xl font-semibold text-slate-900">Application details</h2>
          <dl class="mt-4 space-y-3 text-sm text-slate-600">
            <div class="flex justify-between gap-4"><dt>Company</dt><dd class="font-medium text-slate-900">{{ application.companyName }}</dd></div>
            <div class="flex justify-between gap-4"><dt>Status</dt><dd class="font-medium text-slate-900">{{ application.status }}</dd></div>
            <div class="flex justify-between gap-4"><dt>Location</dt><dd class="font-medium text-slate-900">{{ application.location || '—' }}</dd></div>
            <div class="flex justify-between gap-4"><dt>Applied</dt><dd class="font-medium text-slate-900">{{ application.appliedOn | date }}</dd></div>
          </dl>
        </section>

        <aside class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <h2 class="text-xl font-semibold text-slate-900">Notes</h2>
          <p class="mt-3 text-sm leading-6 text-slate-600">{{ application.notes || 'No notes yet.' }}</p>
        </aside>
      </div>
    </div>

    <ng-template #loading>
      <div class="text-sm text-slate-500">Loading application...</div>
    </ng-template>
  `,
})
export class JobApplicationDetailPage implements OnInit {
  protected readonly jobApplicationService = inject(JobApplicationService);
  private readonly route = inject(ActivatedRoute);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.jobApplicationService.loadApplicationById(id);
    }
  }
}
