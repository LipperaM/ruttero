import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { vi } from 'vitest';
import { RegisterComponent } from './register.component';
import { AuthService } from '../../services/auth.service';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let mockAuthService: any;
  let mockRouter: any;

  beforeEach(async () => {
    mockAuthService = {
      signup: vi.fn()
    };
    mockRouter = {
      navigate: vi.fn()
    };

    await TestBed.configureTestingModule({
      imports: [RegisterComponent],
      providers: [
        { provide: AuthService, useValue: mockAuthService },
        { provide: Router, useValue: mockRouter }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to login after successful registration', () => {
    // Set up the mock to return a successful response
    mockAuthService.signup.mockReturnValue(of({ success: true }));

    // Set up valid credentials
    component.credentials = {
      name: 'Test',
      lastname: 'User',
      email: 'test@example.com',
      password: '123456',
      confirmPassword: '123456'
    };

    // Execute the registration
    component.onSubmit();

    // Verify navigation to login
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/login']);
  });
});
