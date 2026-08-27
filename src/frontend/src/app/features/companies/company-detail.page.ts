import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CompanyService } from './company.service';

@Component({
  selector: 'app-company-detail-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div *ngIf="companyService.selectedCompany() as company; else loading" class="space-y-6">
      <div class="flex items-center justify-between">
        <div>
          <p class="text-sm font-medium text-sky-700">Company</p>
          <h1 class="text-3xl font-bold text-slate-900">{{ company.name }}</h1>
        </div>
        <a routerLink="/companies" class="rounded-xl border border-slate-200 bg-white px-4 py-2 text-sm font-medium text-slate-700 hover:bg-slate-50">Back to companies</a>
      </div>

      <div class="grid gap-6 lg:grid-cols-[1.2fr_0.8fr]">
        <section class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <h2 class="text-xl font-semibold text-slate-900">Company overview</h2>
          <dl class="mt-4 space-y-3 text-sm text-slate-600">
            <div class="flex justify-between gap-4"><dt>Industry</dt><dd class="font-medium text-slate-900">{{ company.industry || '—' }}</dd></div>
            <div class="flex justify-between gap-4"><dt>Location</dt><dd class="font-medium text-slate-900">{{ company.location || '—' }}</dd></div>
            <div class="flex justify-between gap-4"><dt>Website</dt><dd class="font-medium text-slate-900">{{ company.websiteUrl || '—' }}</dd></div>
            <div class="flex justify-between gap-4"><dt>Applications</dt><dd class="font-medium text-slate-900">{{ company.applicationCount ?? 0 }}</dd></div>
          </dl>
        </section>

        <aside class="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <h2 class="text-xl font-semibold text-slate-900">Notes</h2>
          <p class="mt-3 text-sm leading-6 text-slate-600">{{ company.notes || 'No notes available yet.' }}</p>
        </aside>
      </div>
    </div>

    <ng-template #loading>
      <div class="text-sm text-slate-500">Loading company...</div>
    </ng-template>
  `,
})
export class CompanyDetailPage implements OnInit {
  protected readonly companyService = inject(CompanyService);
  private readonly route = inject(ActivatedRoute);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.companyService.loadCompanyById(id);
    }
  }
}
