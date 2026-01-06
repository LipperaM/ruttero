import { Injectable } from '@angular/core';
import { BehaviorSubject, from, Observable, firstValueFrom } from 'rxjs';
import { createClient, SupabaseClient, User, Session } from '@supabase/supabase-js';
import { filter, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private supabase: SupabaseClient;
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  private currentSessionSubject = new BehaviorSubject<Session | null>(null);
  private sessionLoadedSubject = new BehaviorSubject<boolean>(false);
  
  public currentUser$ = this.currentUserSubject.asObservable();
  public currentSession$ = this.currentSessionSubject.asObservable();
  public sessionLoaded$ = this.sessionLoadedSubject.asObservable();

  constructor() {
    // Initialize Supabase client
    this.supabase = createClient(
      'http://localhost',
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxvY2FsaG9zdCIsInJvbGUiOiJhbm9uIiwiaWF0IjoxNjQxNzY5MjAwLCJleHAiOjE5NTczNDUyMDB9.dc6hdXRbT-LTv3JN6cQYRGAkVZAJJOWfkTXeBKkWVQg',
      {
        auth: {
          autoRefreshToken: true,
          persistSession: true,
          detectSessionInUrl: false,
          storageKey: 'supabase-auth'
        },
        global: {
          headers: {
            'Content-Type': 'application/json'
          }
        }
      }
    );

    // Listener for auth state changes
    this.supabase.auth.onAuthStateChange((event, session) => {
      console.log('Auth state changed:', event);
      this.currentSessionSubject.next(session);
      this.currentUserSubject.next(session?.user ?? null);
    });

    // Load initial session
    this.loadSession();
  }

  private async loadSession() {
    try {
      const { data: { session } } = await this.supabase.auth.getSession();
      if (session) {
        this.currentSessionSubject.next(session);
        this.currentUserSubject.next(session.user);
      }
    } catch (error) {
      console.error('Error loading session:', error);
    } finally {
      this.sessionLoadedSubject.next(true);
    }
  }

  async waitForSessionLoad(): Promise<void> {
    await firstValueFrom(
      this.sessionLoaded$.pipe(
        filter(loaded => loaded),
        take(1)
      )
    );
  }

  login(email: string, password: string): Observable<{ user: User | null; session: Session | null }> {
    return from(
      this.supabase.auth.signInWithPassword({ email, password })
        .then(({ data, error }) => {
          if (error) throw error;
          return { user: data.user, session: data.session };
        })
    );
  }

  signup(email: string, password: string, metadata?: { username?: string }): Observable<{ user: User | null; session: Session | null }> {
    return from(
      this.supabase.auth.signUp({
        email,
        password,
        options: {
          data: metadata
        }
      }).then(({ data, error }) => {
        if (error) throw error;
        return { user: data.user, session: data.session };
      })
    );
  }

  logout(): Observable<void> {
    return from(
      this.supabase.auth.signOut().then(({ error }) => {
        if (error) throw error;
      })
    );
  }

  recoverPassword(email: string): Observable<void> {
    return from(
      this.supabase.auth.resetPasswordForEmail(email, {
        redirectTo: 'http://localhost/reset-password'
      }).then(({ error }) => {
        if (error) throw error;
      })
    );
  }

  getToken(): string | null {
    const session = this.currentSessionSubject.value;
    return session?.access_token ?? null;
  }

  getRefreshToken(): string | null {
    const session = this.currentSessionSubject.value;
    return session?.refresh_token ?? null;
  }

  isAuthenticated(): boolean {
    return !!this.currentSessionSubject.value;
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  refreshSession(): Observable<Session | null> {
    return from(
      this.supabase.auth.refreshSession().then(({ data, error }) => {
        if (error) throw error;
        return data.session;
      })
    );
  }
}
