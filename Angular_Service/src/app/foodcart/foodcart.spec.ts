import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Foodcart } from './foodcart';

describe('Foodcart', () => {
  let component: Foodcart;
  let fixture: ComponentFixture<Foodcart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Foodcart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Foodcart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
