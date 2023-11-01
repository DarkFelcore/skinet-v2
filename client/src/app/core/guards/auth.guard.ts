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
      // If the user logs in he/she will be redirected to the page they requested as a non-logged in user, but now as a logged in user (access granted).
      router.navigate(["/auth/login"], { queryParams: { returnUrl: state.url } });
      return false;
    })
  )

};
