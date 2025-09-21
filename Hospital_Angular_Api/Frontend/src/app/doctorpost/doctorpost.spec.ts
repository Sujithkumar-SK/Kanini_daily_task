import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Doctorpost } from './doctorpost';

describe('Doctorpost', () => {
  let component: Doctorpost;
  let fixture: ComponentFixture<Doctorpost>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Doctorpost]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Doctorpost);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
