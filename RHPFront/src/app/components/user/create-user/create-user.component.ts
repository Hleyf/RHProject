import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { passwordValidator } from '../../../shared/password.validator';
import { Router } from '@angular/router';
import { PlayerService } from '../../../services/player.service';
import { IPlayer } from '../../../models/player.model';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [ReactiveFormsModule],
  providers: [],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent implements OnInit {

  form = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(6)]),
    confirmPassword: new FormControl('', [Validators.required])}, 
    passwordValidator
);

  constructor(private service: PlayerService, private router: Router) {}
  ngOnInit(): void {
    
  }

  onSubmit(): void {
    const player: IPlayer = this.validatePlayerForm();     

      this.service.createUser(player).then((status) => {
        if(status === 201) {
          this.router.navigate(['/login']);
        } else {
          this.form.setErrors({
            invalidLogin: true
          });
        }
      });
  }
  

  validatePlayerForm(): IPlayer {
    if(this.form.valid) {
      return {
        name: this.form.controls.name.value!,
        email: this.form.controls.email.value!,
        password: this.form.controls.password.value!
      }
    }
    throw new Error('Invalid form');
  }
}
