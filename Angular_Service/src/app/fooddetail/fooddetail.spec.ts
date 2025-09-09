import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Fooddetail } from './fooddetail';

describe('Fooddetail', () => {
  let component: Fooddetail;
  let fixture: ComponentFixture<Fooddetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Fooddetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Fooddetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
