import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Foodlist } from './foodlist';

describe('Foodlist', () => {
  let component: Foodlist;
  let fixture: ComponentFixture<Foodlist>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Foodlist]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Foodlist);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
