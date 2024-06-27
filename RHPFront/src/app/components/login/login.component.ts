import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router, RouterLink } from '@angular/router';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });

  errorMessage = null;

  constructor(private formBuilder: FormBuilder, private service: AuthService, private playerService: PlayerService ,private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
  this.service.login(this.form.value).subscribe({
    next: (data) => {
      if(data) {
        this.playerService.getLoggedPlayer();
        this.router.navigate(['/home']);
      }
    },
    error: (error) => {
      this.errorMessage = error.message;
    }
  })
  }


}
