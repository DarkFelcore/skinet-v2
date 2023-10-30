import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';

export const AuthGuard: CanActivateFn = (route, state) => {
  const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  return authService.currentUser$.pipe(
    map(auth => {
      if(auth) return true;
      router.navigate(["/auth/login"], { queryParams: { returnUrl: state.url } });
      return false;
    })
  )

};
