import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core'; import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @ViewChild('emailInput') emailInput!: ElementRef<HTMLInputElement>;
  credentials = {
    email: '',
    password: ''
  };

  isLoading = false;
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  onSubmit(): void {
    if (!this.credentials.email || !this.credentials.password) {
      this.errorMessage = 'Por favor, completa todos los campos';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService
      .login(this.credentials.email, this.credentials.password)
      .subscribe({
        next: (response) => {
          console.log('Login exitoso:', response);
          this.isLoading = false;
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error('Error en login:', error);
          this.errorMessage =
            error?.message || 'Error al iniciar sesión. Verifica tus credenciales.';
          this.isLoading = false;

          this.cdr.detectChanges();

          // Focus back to email input after error
          setTimeout(() => {
            this.emailInput.nativeElement.focus();
          });
        }
      });
  }
}
