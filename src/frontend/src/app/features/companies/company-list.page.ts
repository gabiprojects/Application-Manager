import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CompanyService } from './company.service';

@Component({
  selector: 'app-company-list-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="space-y-6">
      <div class="flex items-center justify-between">
        <div>
          <p class="text-sm font-medium text-sky-700">Companies</p>
          <h1 class="text-3xl font-bold text-slate-900">All companies</h1>
        </div>
      </div>

      <div *ngIf="companyService.loading(); else list" class="text-sm text-slate-500">Loading companies...</div>

      <ng-template #list>
        <div class="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
          <a *ngFor="let company of companyService.companies()" [routerLink]="['/companies', company.id]" class="block rounded-2xl border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-0.5 hover:shadow-md">
            <div class="flex items-start justify-between gap-3">
              <div>
                <h2 class="text-lg font-semibold text-slate-900">{{ company.name }}</h2>
                <p *ngIf="company.industry" class="mt-1 text-sm text-slate-500">{{ company.industry }}</p>
              </div>
              <span class="rounded-full bg-sky-100 px-2.5 py-1 text-xs font-medium text-sky-700">{{ company.applicationCount ?? 0 }}</span>
            </div>
            <div *ngIf="company.location || company.websiteUrl" class="mt-4 space-y-1 text-sm text-slate-600">
              <p *ngIf="company.location">📍 {{ company.location }}</p>
              <p *ngIf="company.websiteUrl">🌐 {{ company.websiteUrl }}</p>
            </div>
          </a>
        </div>
      </ng-template>
    </div>
  `,
})
export class CompanyListPage implements OnInit {
  protected readonly companyService = inject(CompanyService);

  ngOnInit(): void {
    this.companyService.loadCompanies();
  }
}
