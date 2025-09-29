import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimezoneService {

  convertToIST(utcDate: string | Date): string {
    if (!utcDate) return 'Not Available';
    
    const date = new Date(utcDate);
    return date.toLocaleString('en-IN', { 
      timeZone: 'Asia/Kolkata',
      year: 'numeric',
      month: 'short', 
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  convertToISTShort(utcDate: string | Date): string {
    if (!utcDate) return 'Not Available';
    
    const date = new Date(utcDate);
    return date.toLocaleString('en-IN', { 
      timeZone: 'Asia/Kolkata',
      year: 'numeric',
      month: 'short', 
      day: 'numeric'
    });
  }

  convertToISTMedium(utcDate: string | Date): string {
    if (!utcDate) return 'Not Available';
    
    const date = new Date(utcDate);
    return date.toLocaleString('en-IN', { 
      timeZone: 'Asia/Kolkata',
      year: 'numeric',
      month: 'long', 
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }
}