import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HallPreviewComponent } from './hall-preview.component';

describe('HallPreviewComponent', () => {
  let component: HallPreviewComponent;
  let fixture: ComponentFixture<HallPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HallPreviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HallPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
