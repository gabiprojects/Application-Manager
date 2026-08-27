import { Injectable, computed, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { CompanyClient, CompanyDetailDto, CompanyDto, CreateCompanyRequest, UpdateCompanyRequest } from '../../generated/api';

@Injectable({ providedIn: 'root' })
export class CompanyService {
  private readonly api = inject(CompanyClient);

  private readonly _companies = signal<CompanyDto[]>([]);
  private readonly _selectedCompany = signal<CompanyDetailDto | null>(null);
  private readonly _loading = signal(false);
  private readonly _error = signal<string | null>(null);

  readonly companies = this._companies.asReadonly();
  readonly selectedCompany = this._selectedCompany.asReadonly();
  readonly loading = this._loading.asReadonly();
  readonly error = this._error.asReadonly();

  readonly companyCount = computed(() => this._companies().length);

  loadCompanies(): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .getAll()
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: (companies) => this._companies.set(companies),
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  loadCompanyById(id: string): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .getById(id)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: (company) => this._selectedCompany.set(company),
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  createCompany(request: CreateCompanyRequest): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .create(request)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: (company) => {
          this._companies.update((companies) => [company, ...companies]);
          this._selectedCompany.set({
            ...company,
            applicationCount: 0,
            contactPersonCount: 0,
          });
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  updateCompany(id: string, request: UpdateCompanyRequest): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .update(id, request)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: () => {
          this._companies.update((companies) =>
            companies.map((company) =>
              company.id === id
                ? {
                    ...company,
                    name: request.name,
                    websiteUrl: request.websiteUrl,
                    industry: request.industry,
                    location: request.location,
                    notes: request.notes,
                    updatedAtUtc: new Date(),
                  }
                : company,
            ),
          );

          const current = this._selectedCompany();
          if (current?.id === id) {
            this._selectedCompany.set({
              ...current,
              name: request.name,
              websiteUrl: request.websiteUrl,
              industry: request.industry,
              location: request.location,
              notes: request.notes,
              updatedAtUtc: new Date(),
            });
          }
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  deleteCompany(id: string): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .delete(id)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: () => {
          this._companies.update((companies) => companies.filter((company) => company.id !== id));
          const current = this._selectedCompany();
          if (current?.id === id) {
            this._selectedCompany.set(null);
          }
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  private extractErrorMessage(error: unknown): string {
    if (typeof error === 'object' && error !== null) {
      const value = error as {
        message?: string;
        title?: string;
        detail?: string;
        error?: { message?: string; title?: string; detail?: string };
      };

      if (value.error) {
        if (value.error.detail) return value.error.detail;
        if (value.error.title) return value.error.title;
        if (value.error.message) return value.error.message;
      }

      if (value.detail) return value.detail;
      if (value.title) return value.title;
      if (value.message) return value.message;
    }

    return 'Something went wrong while communicating with the server.';
  }
}
